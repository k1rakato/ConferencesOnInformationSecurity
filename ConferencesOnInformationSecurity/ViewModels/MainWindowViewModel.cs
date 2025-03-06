using Avalonia.Controls;
using ConferencesOnInformationSecurity.Models;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl uc;

        public static MainWindowViewModel Self;

        Organizer authOrganizer;

        public UserControl Uc { get => uc; set => this.RaiseAndSetIfChanged(ref uc, value); }
        public Organizer AuthOrganizer { get => authOrganizer; set => this.RaiseAndSetIfChanged(ref authOrganizer, value); }
        public MainWindowViewModel()
        {
            uc = new SharedView();
            Self = this;
        }
    }
}
