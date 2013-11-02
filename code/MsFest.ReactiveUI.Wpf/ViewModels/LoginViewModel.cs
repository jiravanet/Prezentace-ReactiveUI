using System;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class LoginViewModel : ReactiveObject
    {
        public LoginViewModel(IAuthentication authentication = null)
        {
            authentication = authentication ?? new Authentication();
            var canLogin = this.WhenAny(x => x.LoginName,
                x => x.Password,
                (l, p) => !String.IsNullOrWhiteSpace(l.Value) && !String.IsNullOrWhiteSpace(p.Value));
            LoginCommand = new ReactiveCommand(canLogin);
            var loggedIn = LoginCommand.RegisterAsyncTask(_ => authentication.AuthenticateAsync(LoginName,
                Password).
                                                                              ContinueWith(t => t.Result == AuthenticationResult.Authenticated
                                                                                                    ? "Přihlášen"
                                                                                                    : "Nepřihlášen"));
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
    }
}