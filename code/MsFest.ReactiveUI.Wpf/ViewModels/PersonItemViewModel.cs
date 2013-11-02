using System;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class PersonItemViewModel : ReactiveObject
    {

        public PersonItemViewModel(string firstName, string lastName, int age)
        {
            this.WhenAny(x => x.FirstName,
                x => x.LastName,
                (f, l) => String.Format("{0}, {1}",
                    l.Value,
                    f.Value)).
                                  ToProperty(this,
                                      x => x.FullName,
                                      out fullName);
            FirstName = firstName;
            LastName = lastName;
            Age = age;

        }

        string firstName;

        public string FirstName
        {
            get { return firstName; }
            set
            {
                this.RaiseAndSetIfChanged(ref firstName,
                    value);
            }
        }

        string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                this.RaiseAndSetIfChanged(ref lastName,
                    value);
            }
        }

        int age;

        public int Age
        {
            get { return age; }
            set
            {
                this.RaiseAndSetIfChanged(ref age,
                    value);
            }
        }

        readonly ObservableAsPropertyHelper<string> fullName; 
        public string FullName
        {
            get { return fullName.Value; }
        }
    }
}