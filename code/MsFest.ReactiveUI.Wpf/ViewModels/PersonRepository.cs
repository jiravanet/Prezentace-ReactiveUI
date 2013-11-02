using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public interface IPersonRepository
    {
        void AddPerson(Person person);

        Task<List<Person>> RetrievePersonsAsync();
    }

    public class PersonRepository : IPersonRepository
    {
        readonly List<Person> persons;
        public PersonRepository()
        {
            persons = new List<Person>();
            FillPersons();
        }

        void FillPersons()
        {
            for (var i = 0; i < 10; i++)
            {
                AddPerson(new Person()
                {
                    Age = 18 + i,
                    FirstName = "First " + i,
                    LastName = "Last " + i
                });
            }
        }

        public void AddPerson(Person person)
        {
            person.Id = persons.Count + 1;
            persons.Add(person);
        }

        public Task<List<Person>> RetrievePersonsAsync()
        {
            var tcs = new TaskCompletionSource<List<Person>>();
            new TaskFactory().StartNew(() => tcs.SetResult(persons));
            return tcs.Task;
        }
    }

    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}