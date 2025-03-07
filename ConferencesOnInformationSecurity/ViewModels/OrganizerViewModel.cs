using System;
using System.Collections.Generic;
using ConferencesOnInformationSecurity.Models;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
	public class OrganizerViewModel : ViewModelBase
	{
		string greeting;
		Organizer organizers;
		string genderOrg;

        public string Greeting { get => greeting; set => this.RaiseAndSetIfChanged(ref greeting, value); }
        public string GenderOrg { get => genderOrg; set => this.RaiseAndSetIfChanged(ref greeting, value); }
        public Organizer Organizers { get => organizers; set => this.RaiseAndSetIfChanged(ref organizers, value); }

        public OrganizerViewModel()
		{
            Organizers = MainWindowViewModel.Self.AuthOrganizer;
			
			if(DateTime.Now.Hour >= 9 || DateTime.Now.Hour <= 11)
			{
				Greeting = "Доброе утро!";
			}
			if(DateTime.Now.Hour > 11 || DateTime.Now.Hour <= 18)
			{
				Greeting = "Добрый день!";
			}
			if(DateTime.Now.Hour > 18 || DateTime.Now.Hour <= 24)
			{
				Greeting = "Добрый вечер!";
			}
			else
			{
				Greeting = "Доброй ночи!";
			}

			if (Organizers.Gender.Gendername == "мужской") { genderOrg = "Mrs"; }
			else {genderOrg = "Ms";}

        }


		public void NavigateToReg()
		{
			MainWindowViewModel.Self.Uc = new RegistrationPage();
        }
    }
}