using System;
using System.Linq;
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
            NewPersonCommand = new ReactiveCommand(null);
            NewPersonCommand.RegisterAsyncAction(_ => { }).Subscribe(_ => HostScreen.Router.Navigate.Execute(new PersonAddViewModel(HostScreen)));
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
        public ReactiveCommand NewPersonCommand { get; protected set; }

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