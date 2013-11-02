using System;
using System.Threading.Tasks;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public interface IAuthentication
    {
        AuthenticationResult Authenticate(string username, string password);
        Task<AuthenticationResult> AuthenticateAsync(string username, string password);
    }

    public class Authentication : IAuthentication
    {
        public AuthenticationResult Authenticate(string username, string password)
        {
            return username == "jarda" && password == "heslo"
                       ? AuthenticationResult.Authenticated
                       : AuthenticationResult.Nonauthenticated;
        }

        public Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            var tcs = new TaskCompletionSource<AuthenticationResult>();
            new TaskFactory().StartNew(() =>
            {
                Task.Delay(TimeSpan.FromSeconds(2)).
                     ContinueWith(t => tcs.SetResult(Authenticate(username,
                         password)));
            });
            return tcs.Task;
        }
    }

    public enum AuthenticationResult
    {
        Authenticated,
        Nonauthenticated
    }
}