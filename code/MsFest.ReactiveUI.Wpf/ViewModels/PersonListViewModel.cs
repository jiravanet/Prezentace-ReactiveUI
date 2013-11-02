using System.Linq;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class PersonListViewModel : ReactiveObject
    {
        public PersonListViewModel(IPersonRepository personRepository = null)
        {
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
    }
}