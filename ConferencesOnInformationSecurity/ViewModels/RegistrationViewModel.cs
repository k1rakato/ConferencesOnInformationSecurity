using System;
using System.Collections.Generic;
using ConferencesOnInformationSecurity.Models;
using System.Data;
using ReactiveUI;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using System.IO;
using System.Threading.Tasks;
using Avalonia;
using System.Text.RegularExpressions;

namespace ConferencesOnInformationSecurity.ViewModels
{
	public class RegistrationViewModel : ViewModelBase
	{
        // Поля для хранения данных
        private int numberID;
        private string fio;
        private string email;
        private string phoneNumber;
        private string password;
        private string image;
        private string info;
        private string confirmPassword;
        private List<Gender> genders;
        private Gender selectedGender;
        private List<string> roles;
        private string selectedRole;
        private List<Event> typeOfEvents;
        private Event selectedTypeOfEvent;
        private List<Meropriatie> meropriaties;
        private Event selectedEvent;
        private bool isVisible;
        private bool isAttach;
        private string _passwordChar;

        public int NumberID { get => numberID; set => this.RaiseAndSetIfChanged(ref numberID, value); }
        public string Fio { get => fio; set => this.RaiseAndSetIfChanged(ref fio, value); }
        public string Email { get => email; set => this.RaiseAndSetIfChanged(ref email, value); }
        public string PhoneNumber { get => phoneNumber; set => this.RaiseAndSetIfChanged(ref phoneNumber, value); }
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value);  }
        public string Image { get => image; set => this.RaiseAndSetIfChanged(ref image, value); }
        public string Message { get => info; set => this.RaiseAndSetIfChanged(ref info, value); }
        public string ConfirmPassword { get => confirmPassword; set => this.RaiseAndSetIfChanged(ref confirmPassword, value); }
        public List<Gender> Genders { get => genders; set => this.RaiseAndSetIfChanged(ref genders, value); }
        public Gender SelectedGender { get => selectedGender; set => this.RaiseAndSetIfChanged(ref selectedGender, value); }
        public List<string> Roles { get => roles; set => this.RaiseAndSetIfChanged(ref roles, value); }
        public string SelectedRole { get => selectedRole; set => this.RaiseAndSetIfChanged(ref selectedRole, value); }
        public List<Event> TypeOfEvents { get => typeOfEvents; set { this.RaiseAndSetIfChanged(ref typeOfEvents, value); FiltredMeropriaties(); } }
        public Event SelectedTypeOfEvent { get => selectedTypeOfEvent; set => this.RaiseAndSetIfChanged(ref selectedTypeOfEvent, value); }
        public List<Meropriatie> Meropriaties { get => meropriaties; set => this.RaiseAndSetIfChanged(ref meropriaties, value); }
        public Event SelectedEvent { get => selectedEvent; set => this.RaiseAndSetIfChanged(ref selectedEvent, value); }
        public bool IsVisible {get => isVisible; set { this.RaiseAndSetIfChanged(ref isVisible, value); PasswordChar = value ? "" : "*";} }
        public bool IsAttach { get => isAttach; set => this.RaiseAndSetIfChanged(ref isAttach, value); }
        public string PasswordChar { get => _passwordChar; set => this.RaiseAndSetIfChanged(ref _passwordChar, value); }

        /// <summary>
        /// Регулярное выражение для проверки email.
        /// </summary>
        Regex EmailMask = new Regex("^\\S+@\\S+\\.\\S+$");

        /// <summary>
        /// Конструктор ViewModel.
        /// </summary>
        public RegistrationViewModel()
        {
            Random rnd = new Random();
            NumberID = rnd.Next(10000, 99999); 

       
            Genders = db.Genders.ToList();
            Genders.Add(new Gender() { Gendername = "Выберите пол", Idgender = 0 });
            Genders = Genders.OrderBy(x => x.Idgender).ToList();
            SelectedGender = Genders.First(x => x.Idgender == 0);

            Roles = new List<string>() {"Жюри", "Модератор", "Выберите роль" };
            SelectedRole = Roles.FirstOrDefault(x => x == "Выберите роль");

            TypeOfEvents = db.Events.ToList();
            Meropriaties = db.Meropriaties.ToList();

            IsVisible = false;
            IsAttach = false;
            Image = "";
        }

        /// <summary>
        /// Метод для выбора фото пользователя.
        /// </summary>
        public async Task SelectFoto()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop || desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Отсутствует экземпляр StorageProvider.");

            var files = await provider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Выберите фото",
                AllowMultiple = false
            });

            if (files.Count > 0)
            {
                await using var readStream = await files[0].OpenReadAsync();
                var savePath = Path.Combine(Environment.CurrentDirectory, "Image", NumberID + ".png");
                await using var writeStream = new FileStream(savePath, FileMode.Create);
                await readStream.CopyToAsync(writeStream);
            }
            Image = NumberID + ".png";
        }

        /// <summary>
        /// Фильтрация мероприятий по выбранному типу.
        /// </summary>
        private void FiltredMeropriaties()
        {
            Meropriaties = db.Meropriaties.Where(x => x.Event == SelectedTypeOfEvent).ToList();
        }

        /// <summary>
        /// Метод для возврата на страницу организатора.
        /// </summary>
        public void Back()
        {
            MainWindowViewModel.Self.Uc = new OrganizerPage();
        }

        /// <summary>
        /// Метод для сохранения данных пользователя.
        /// </summary>
        public void Save()
        {
            if (string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword) || string.IsNullOrWhiteSpace(Fio) ||
                SelectedGender.Idgender == 0 || string.IsNullOrWhiteSpace(PhoneNumber) || SelectedRole == "Выберите роль" || SelectedTypeOfEvent.Idevent == 0)
            {
                Message = "Заполните поля";
                return;
            }

            if (!PhoneNumber.Any(char.IsDigit))
            {
                Message = "Некорректный номер телефона";
                return;
            }

            if (!EmailMask.IsMatch(Email))
            {
                Message = "Некорректная почта";
                return;
            }

            if (Password.Length < 6 || !Password.Any(char.IsUpper) || !Password.Any(char.IsLower) || !Password.Any(char.IsDigit) || Password.All(char.IsLetterOrDigit))
            {
                Message = "Пароль должен содержать минимум 6 символов, одну заглавную и одну строчную букву, цифру и спецсимвол.";
                return;
            }

            if (Password != ConfirmPassword)
            {
                Message = "Пароли не совпадают";
                return;
            }

            string[] words = Fio.Split(" ");
            if (SelectedRole == "Жюри")
            {
                Jury jury = new Jury()
                {
                    Juryid = NumberID,
                    Lastname = words[0],
                    Firstname = words[1],
                    Patronymic = words[2],
                    Email = Email,
                    Phone = PhoneNumber,
                    Password = Password,
                    Gender = SelectedGender,
                    Directionsid = null 
                };
                if (Image != "")
                {
                    jury.Foto = Image;
                }
                try
                {
                    db.Juries.Add(jury);
                    db.SaveChanges();
                    MainWindowViewModel.Self.Uc = new OrganizerPage();
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }
            }

            if (SelectedRole == "Модератор")
            {
                Moder moder = new Moder()
                {
                    Moderid = NumberID,
                    Lastname = words[0],
                    Firstname = words[1],
                    Patronymic = words[2],
                    Email = Email,
                    Phone = PhoneNumber,
                    Password = Password,
                    Event = selectedTypeOfEvent,
                    Gender = SelectedGender,
                    Directionsid = null
                };
                if(Image != "")
                {
                    moder.Foto = Image;
                }
                try
                {
                    db.Events.Attach(moder.Event);
                    db.Genders.Attach(moder.Gender);
                    db.Moders.Add(moder);
                    db.SaveChanges();
                    MainWindowViewModel.Self.Uc = new OrganizerPage();
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                }

                // Код сохранения данных в зависимости от выбранной роли
            }
        }
    }
}