using System.Threading.Tasks;
using MsFest.ReactiveUI.Wpf.ViewModels;

namespace MsFest.ReactiveUI.Tests.ViewModels
{
    internal class TestAuthentication : IAuthentication
    {
        public AuthenticationResult Authenticate(string username, string password)
        {
            return username == "jarda" && password == "heslo"
                       ? AuthenticationResult.Authenticated
                       : AuthenticationResult.Failed;
        }

        public Task<AuthenticationResult> AuthenticateAsync(string username, string password)
        {
            var task = new Task<AuthenticationResult>(() => Authenticate(username, password));
            task.Start();
            return task;
        }
    }
}