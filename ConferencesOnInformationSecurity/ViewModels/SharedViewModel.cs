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
	public class SharedViewModel : ViewModelBase
	{
		List<Meropriatie> meropriaties;
		List<Meropriatie> meropriaties0;

        public List<Meropriatie> Meropriaties { get => meropriaties; set => this.RaiseAndSetIfChanged(ref meropriaties, value); }
        public List<Meropriatie> Meropriaties0 { get => meropriaties0; set => this.RaiseAndSetIfChanged(ref meropriaties0, value); }
        
        Boolean isVisible;
        public bool IsVisible { get => isVisible; set => this.RaiseAndSetIfChanged(ref isVisible, value); }

        List<string> dateOfmeropriatie;
        public List<string> DateOfmeropriatie { get => dateOfmeropriatie; set => this.RaiseAndSetIfChanged(ref dateOfmeropriatie, value); }
        

        string selectDate;
        public string SelectDate { get => selectDate; set { this.RaiseAndSetIfChanged(ref selectDate, value); Search(); } }


        List<Event> typeEvents;
        public List<Event> TypeEvents { get => typeEvents; set => this.RaiseAndSetIfChanged(ref typeEvents, value); }
      

        Event selectedEvent;
        public Event SelectedEvent { get => selectedEvent; set { this.RaiseAndSetIfChanged(ref selectedEvent, value); Search(); } }



        private void Search()
        {
            Meropriaties = Meropriaties0;
            if (SelectedEvent.Idevent != 0 && SelectDate != "Выберете дату")
            {
                Meropriaties = Meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent && x.Datemeropriatie.ToString() == SelectDate).ToList();
            }
            else
            {
                if (SelectedEvent.Idevent != 0)
                {
                    Meropriaties = Meropriaties0.Where(x => x.Eventid == SelectedEvent.Idevent).ToList(); 
                }
                if(SelectDate != "Выберете дату")
                {
                    Meropriaties = Meropriaties0.Where(x => x.Datemeropriatie.ToString() == SelectDate).ToList();
                }
            }
            IsVisible = Meropriaties.Count == 0;
        }

        public SharedViewModel() 
        {
            Meropriaties0 = db.Meropriaties.Include(x => x.Event).ToList();
            Meropriaties = Meropriaties0;
            TypeEvents = db.Events.ToList();
            TypeEvents.Add(new Event() { Idevent = 0, Eventname = "Выберете тип мероприятия" });
            TypeEvents = TypeEvents.OrderBy(x => x.Idevent).ToList();
            SelectedEvent = TypeEvents.FirstOrDefault(x => x.Idevent == 0);
            IsVisible = false;
            DateOfmeropriatie = Meropriaties0.Select(x => x.Datemeropriatie.ToString()).Distinct().Order().ToList();
            DateOfmeropriatie.Insert(0, "Выберете дату");
            SelectDate = DateOfmeropriatie[0];
        }    

        public void NavigationToAuth()
        {
            MainWindowViewModel.Self.Uc = new AauthView();
        }
    }
}