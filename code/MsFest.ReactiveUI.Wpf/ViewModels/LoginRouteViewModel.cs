using System;
using System.Reactive.Linq;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class LoginRouteViewModel : ReactiveObject, IRoutableViewModel
    {
        public LoginRouteViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            var authentication = new Authentication();
            var canLogin = this.WhenAny(x => x.LoginName,
                x => x.Password,
                (l, p) => !String.IsNullOrWhiteSpace(l.Value) && !String.IsNullOrWhiteSpace(p.Value));
            LoginCommand = new ReactiveCommand(canLogin);
            var loggedIn = LoginCommand.RegisterAsync(_ => Observable.Start(() =>
            {
                var authenticationResult = authentication.AuthenticateAsync(LoginName,
                    Password).
                                                          Result;
                return authenticationResult == AuthenticationResult.Authenticated
                    ? "Přihlášen"
                    : "Nepřihlášen";
            }));
            loggedIn.Subscribe(s =>
            {
                if (s == "Přihlášen")
                    HostScreen.Router.Navigate.Execute(new PersonListViewModel(HostScreen));
            });
            message = new ObservableAsPropertyHelper<string>(loggedIn,
                s => raisePropertyChanged("Message"));
        }

        string loginName;

        public string LoginName
        {
            get { return loginName; }
            set
            {
                this.RaiseAndSetIfChanged(ref loginName,
                    value);
            }
        }

        string password;

        public string Password
        {
            get { return password; }
            set
            {
                this.RaiseAndSetIfChanged(ref password,
                    value);
            }
        }

        ObservableAsPropertyHelper<string> message;

        public string Message
        {
            get { return message.Value; }
        }

        public ReactiveCommand LoginCommand { get; protected set; }

        public string UrlPathSegment
        {
            get { return "login"; }
        }

        public IScreen HostScreen { get; private set; }
    }
}