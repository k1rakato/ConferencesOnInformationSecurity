using Avalonia.Controls;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        UserControl uc;

        public static MainWindowViewModel Self;
        public UserControl Uc { get => uc; set => this.RaiseAndSetIfChanged(ref uc, value); }

        public MainWindowViewModel()
        {
            uc = new SharedView();
            Self = this;
        }
    }
}
