using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using ConferencesOnInformationSecurity.ViewModels;

namespace ConferencesOnInformationSecurity;

public partial class SharedView : UserControl
{
    public SharedView()
    {
        InitializeComponent();
        DataContext = new SharedViewModel();
    }
}