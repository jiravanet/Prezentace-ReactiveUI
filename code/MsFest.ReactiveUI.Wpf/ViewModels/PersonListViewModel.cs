using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class PersonListViewModel : ReactiveObject, IRoutableViewModel
    {
        public PersonListViewModel(IScreen hostScreen, IPersonRepository personRepository = null)
        {
            HostScreen = hostScreen;
            personRepository = personRepository ?? new PersonRepository();
            Persons = new ReactiveList<PersonItemViewModel>();
            RefreshCommand = new ReactiveCommand(null);
            RefreshCommand.RegisterAsync<object>(_ =>
            {
                using (Persons.SuppressChangeNotifications())
                {
                    Persons.Clear();
                    Persons.AddRange(personRepository.RetrievePersonsAsync().
                                                      Result.Select(d => new PersonItemViewModel(d.FirstName,
                                                                             d.LastName,
                                                                             d.Age)));
                }
                return null;
            });
        }

        public ReactiveCommand RefreshCommand { get; protected set; }

        public ReactiveList<PersonItemViewModel> Persons
        {
            get;
            protected set;
        }

        public string UrlPathSegment
        {
            get { return "personlist"; }
        }

        public IScreen HostScreen { get; private set; }
    }
}