using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConferencesOnInformationSecurity.ViewModels;

namespace ConferencesOnInformationSecurity;

public partial class RegistrationPage : UserControl
{
    public RegistrationPage()
    {
        InitializeComponent();
        DataContext = new RegistrationViewModel();
    }
}