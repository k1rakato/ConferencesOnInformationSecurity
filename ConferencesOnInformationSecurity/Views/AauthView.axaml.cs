using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConferencesOnInformationSecurity.ViewModels;

namespace ConferencesOnInformationSecurity;

public partial class AauthView : UserControl
{
    public AauthView()
    {
        InitializeComponent();
        DataContext = new AuthViewModel();
    }
}