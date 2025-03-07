using Avalonia.Controls;
using ConferencesOnInformationSecurity.Models;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    /// <summary>
    /// ViewModel для главного окна приложения.
    /// Управляет текущим отображаемым UserControl и аутентифицированным организатором.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        // Поле для хранения текущего UserControl
        private UserControl uc;

        // Статическая ссылка на текущий экземпляр MainWindowViewModel (паттерн Singleton).
        public static MainWindowViewModel Self;

        // Поле для хранения аутентифицированного организатора
        private Organizer authOrganizer;

        /// <summary>
        /// Свойство для управления текущим UserControl в главном окне.
        /// Позволяет изменять отображаемую страницу.
        /// </summary>
        public UserControl Uc { get => uc; set => this.RaiseAndSetIfChanged(ref uc, value);}

        /// <summary>
        /// Свойство для хранения информации об аутентифицированном организаторе.
        /// </summary>
        public Organizer AuthOrganizer{get => authOrganizer; set => this.RaiseAndSetIfChanged(ref authOrganizer, value);}

        /// <summary>
        /// Конструктор MainWindowViewModel.
        /// Устанавливает начальное представление и сохраняет ссылку на себя (Singleton).
        /// </summary>
        public MainWindowViewModel()
        {
            uc = new SharedView(); // По умолчанию отображается SharedView
            Self = this; // Сохраняем текущий экземпляр в статическое свойство
        }
    }

}
