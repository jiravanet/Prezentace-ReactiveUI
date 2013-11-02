using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class PersonAddViewModel : ReactiveObject, IRoutableViewModel
    {

        public PersonAddViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            var canAdd = this.WhenAny(x => x.FirstName,
                x => x.LastName,
                x => x.Age,
                (f, l, a) => !String.IsNullOrWhiteSpace(f.Value) && !String.IsNullOrWhiteSpace(l.Value) && a.Value > 17 && a.Value < 120);
            AddCommand = new ReactiveCommand(canAdd);
            var add = AddCommand.RegisterAsync(_ => Observable.Start(() => MessageBus.Current.SendMessage(new Person(FirstName,
                                                                                         LastName,
                                                                                         Age))));
            add.Subscribe(_ => HostScreen.Router.NavigateBack.Execute(null));
        }

        public ReactiveCommand AddCommand { get; protected set; }

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


        public string UrlPathSegment
        {
            get { return "addperson"; }
        }

        public IScreen HostScreen { get; private set; }
    }
}