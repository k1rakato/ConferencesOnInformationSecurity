using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConferencesOnInformationSecurity.ViewModels;

namespace ConferencesOnInformationSecurity;

public partial class OrganizerPage : UserControl
{
    public OrganizerPage()
    {
        InitializeComponent();
        DataContext = new OrganizerViewModel();
    }
}