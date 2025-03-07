using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConferencesOnInformationSecurity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    /// <summary>
    /// ViewModel для управления мероприятиями и их фильтрацией.
    /// </summary>
    public class SharedViewModel : ViewModelBase
    {
        // Приватные поля для хранения данных
        private List<Meropriatie> meropriaties;
        private List<Meropriatie> meropriaties0;
        private bool isVisible;
        private List<string> dateOfMeropriatie;
        private string selectDate;
        private List<Event> typeEvents;
        private Event selectedEvent;

        /// <summary>
        /// Список мероприятий с учетом фильтров.
        /// </summary>
        public List<Meropriatie> Meropriaties { get => meropriaties; private set => this.RaiseAndSetIfChanged(ref meropriaties, value); }

        /// <summary>
        /// Исходный список мероприятий.
        /// </summary>
        public List<Meropriatie> Meropriaties0 { get => meropriaties0; private set => this.RaiseAndSetIfChanged(ref meropriaties0, value); }

        /// <summary>
        /// Определяет, виден ли список мероприятий.
        /// </summary>
        public bool IsVisible { get => isVisible; private set => this.RaiseAndSetIfChanged(ref isVisible, value); }

        /// <summary>
        /// Список доступных дат мероприятий.
        /// </summary>
        public List<string> DateOfMeropriatie { get => dateOfMeropriatie; private set => this.RaiseAndSetIfChanged(ref dateOfMeropriatie, value); }

        /// <summary>
        /// Выбранная дата мероприятия.
        /// </summary>
        public string SelectDate { get => selectDate; set { this.RaiseAndSetIfChanged(ref selectDate, value); Search(); } }

        /// <summary>
        /// Список доступных типов мероприятий.
        /// </summary>
        public List<Event> TypeEvents { get => typeEvents; private set => this.RaiseAndSetIfChanged(ref typeEvents, value); }

        /// <summary>
        /// Выбранное мероприятие.
        /// </summary>
        public Event SelectedEvent { get => selectedEvent; set { this.RaiseAndSetIfChanged(ref selectedEvent, value); Search(); } }

        /// <summary>
        /// Конструктор, выполняющий инициализацию данных.
        /// </summary>
        public SharedViewModel()
        {
            InitializeData();
        }

        /// <summary>
        /// Инициализация данных ViewModel.
        /// </summary>
        private void InitializeData()
        {
            meropriaties0 = db.Meropriaties.Include(x => x.Event).ToList();
            meropriaties = meropriaties0;
            typeEvents = db.Events.ToList();
            typeEvents.Add(new Event() { Idevent = 0, Eventname = "Выберите тип мероприятия" });
            typeEvents = typeEvents.OrderBy(x => x.Idevent).ToList();
            selectedEvent = typeEvents.FirstOrDefault(x => x.Idevent == 0);
            isVisible = false;
            dateOfMeropriatie = meropriaties0.Select(x => x.Datemeropriatie.ToString()).Distinct().Order().ToList();
            dateOfMeropriatie.Insert(0, "Выберите дату");
            selectDate = dateOfMeropriatie[0];
        }

        /// <summary>
        /// Фильтрация списка мероприятий по выбранному типу и/или дате.
        /// </summary>
        private void Search()
        {
            Meropriaties = meropriaties0;

            bool isEventSelected = SelectedEvent?.Idevent != 0;
            bool isDateSelected = SelectDate != "Выберите дату";

            if (isEventSelected && isDateSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent && x.Datemeropriatie.ToString() == SelectDate).ToList();
            }
            else if (isEventSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent).ToList();
            }
            else if (isDateSelected)
            {
                Meropriaties = meropriaties0.Where(x => x.Datemeropriatie.ToString() == SelectDate).ToList();
            }

            IsVisible = Meropriaties.Count == 0;
        }

        /// <summary>
        /// Метод для навигации к окну авторизации.
        /// </summary>
        public void NavigateToAuth()
        {
            MainWindowViewModel.Self.Uc = new AauthView();
        }
    }
}