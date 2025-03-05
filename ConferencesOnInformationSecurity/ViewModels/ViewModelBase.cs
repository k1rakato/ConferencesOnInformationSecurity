using ConferencesOnInformationSecurity.Models;
using ReactiveUI;

namespace ConferencesOnInformationSecurity.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        public Bolshakovmdk01Context db = new Bolshakovmdk01Context();
    }
}
