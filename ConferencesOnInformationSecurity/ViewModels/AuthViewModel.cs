using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using ConferencesOnInformationSecurity.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Tmds.DBus.Protocol;

namespace ConferencesOnInformationSecurity.ViewModels
{
    public class AuthViewModel : ViewModelBase
    {
        // Поля для хранения данных
        string email;
        string password;
        string message;

        bool isVisible = true;
        string capcha;
        string capchaInput;
        Canvas canvasCapcha;
        int counter = 0;

        DispatcherTimer _timer;

        public string Email { get => email; set => this.RaiseAndSetIfChanged(ref email, value); }
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        public string Message { get => message; set => this.RaiseAndSetIfChanged(ref message, value); }
        public bool IsVisible { get => isVisible; set => this.RaiseAndSetIfChanged(ref isVisible, value); }
        public string Capcha { get => capcha; set => this.RaiseAndSetIfChanged(ref capcha, value); }
        public string CapchaInput { get => capchaInput; set => this.RaiseAndSetIfChanged(ref capchaInput, value); }
        public Canvas CanvasCapcha { get => canvasCapcha; set => this.RaiseAndSetIfChanged(ref canvasCapcha, value); }


        /// <summary>
        /// Конструктор класса AuthViewModel.
        /// При создании объекта сразу генерирует капчу.
        /// </summary>
        public AuthViewModel()
        {
            GenerateCapcha();
        }

        /// <summary>
        /// Метод для входа в систему.
        /// Проверяет корректность введённых данных и осуществляет вход.
        /// </summary>
        public void Enter()
        {
            Message = "";

            // Проверка на заполненность полей.
            if (Email == "" || Password == "" || CapchaInput == "")
            {
                counter++;
                Message = "Поля не заполнены";
                GenerateCapcha();
                return;
            }

            // Проверка совпадения капчи.
            if (CapchaInput != Capcha)
            {
                Message = "Капча не совпадает";
                CapchaInput = "";
                GenerateCapcha();
                return;
            }

            // Поиск пользователя в базе данных по введённым email и паролю.
            Client? client = db.Clients.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Organizer organizer = db.Organizers.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Moder? moder = db.Moders.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Jury? jury = db.Juries.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);

            // Если найден хотя бы один пользователь, перенаправляем его на соответствующую страницу.
            if (client != null || organizer != null || moder != null || jury != null)
            {
                if (client != null)
                {
                    MainWindowViewModel.Self.Uc = new ClientPage();
                    return;
                }
                if (organizer != null)
                {
                    MainWindowViewModel.Self.AuthOrganizer = organizer;
                    MainWindowViewModel.Self.Uc = new OrganizerPage();
                    return;
                }
                if (moder != null)
                {
                    MainWindowViewModel.Self.Uc = new ModersPage();
                    return;
                }
                if (jury != null)
                {
                    MainWindowViewModel.Self.Uc = new JuryPage();
                    return;
                }
            }
            else
            {
                // Если пользователь не найден, увеличиваем счётчик ошибок.
                counter++;
                if (counter == 3)
                {
                    // Блокируем систему на 15 секунд после 3 неверных попыток.
                    Message = "Разблокировка системы через 15 секунд";
                    IsVisible = false;
                    _timer.Interval = new TimeSpan(0, 0, 15);
                    _timer.Tick += TimerTick;
                    _timer.Start();
                }
                else
                {
                    Message = "Неверные лд"; // Возможно, ошибка в тексте (может быть, "Неверные логин или пароль"?)
                }
            }

            // Генерируем новую капчу после попытки входа.
            GenerateCapcha();
        }

        /// <summary>
        /// Событие таймера, разблокирующее систему после ожидания.
        /// </summary>
        private void TimerTick(object send, EventArgs e)
        {
            IsVisible = true;
            counter = 0;
        }

        /// <summary>
        /// Генерирует случайную капчу и визуализирует её.
        /// </summary>
        public void GenerateCapcha()
        {
            Random rand = new Random();
            const string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int length = 4;
            Capcha = "";

            // Создаём холст для отображения капчи.
            Canvas canvas = new Canvas()
            {
                Width = 200,
                Height = 90,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Background = new SolidColorBrush(Colors.White)
            };

            // Начальная координата для вывода символов.
            double startX = rand.Next(10, 20);

            // Генерация символов капчи.
            for (int i = 0; i < length; i++)
            {
                TextBlock myTextBlock = new TextBlock()
                {
                    FontSize = 20,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontWeight = FontWeight.Bold
                };

                int index = rand.Next(symbols.Length);
                char result = symbols[index];
                myTextBlock.Text = result.ToString();
                Capcha += Convert.ToString(result);

                Canvas.SetLeft(myTextBlock, startX + (i * 30));
                Canvas.SetTop(myTextBlock, rand.Next(5, 40));
                canvas.Children.Add(myTextBlock);
            }

            // Генерация случайных линий на капче для усложнения распознавания ботами.
            for (int i = 0; i < rand.Next(15, 40); i++)
            {
                Line line = new Line()
                {
                    StartPoint = new Avalonia.Point(rand.Next(200), rand.Next(90)),
                    EndPoint = new Avalonia.Point(rand.Next(200), rand.Next(90)),
                    Stroke = new SolidColorBrush(Colors.Aqua),
                    StrokeThickness = rand.Next(3)
                };
                canvas.Children.Add(line);
            }

            // Сохраняем сгенерированную капчу в свойство CanvasCapcha.
            CanvasCapcha = canvas;
        }

        /// <summary>
        /// Метод для возврата на предыдущую страницу.
        /// </summary>
        public void Back()
        {
            MainWindowViewModel.Self.Uc = new SharedView();
        }

    }
}