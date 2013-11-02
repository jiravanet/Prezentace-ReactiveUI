using MsFest.ReactiveUI.Wpf.ViewModels;
using Xunit;
using Xunit.Extensions;

namespace MsFest.ReactiveUI.Tests.ViewModels
{
    public class LoginViewModelTests
    {
        [Theory]
        [InlineData("", "", false)]
        [InlineData("j", "", false)]
        [InlineData("", "a", false)]
        [InlineData("j", "a", true)]
        public void ShouldChangeLoginCommandDisable(string username, string password, bool canExecute)
        {
            var sut = new LoginViewModel(new TestAuthentication());
            sut.LoginName = username;
            sut.Password = password;
            Assert.Equal(canExecute, sut.LoginCommand.CanExecute(null));
        }

[Fact]
        public void ShouldSetMessageAfterFailedAuthentication()
        {
            var username = "j";
            var password = "a";
            var expected = "Nepřihlášen";
            var sut = new LoginViewModel(new TestAuthentication())
            {
                LoginName = username,
                Password = password
            };
            sut.LoginCommand.Execute(null);
            Assert.Equal(expected, sut.Message);
            
        }
        [Fact]
        public void ShouldSetMessageAfterValidAuthentication()
        {
            var username = "jarda";
            var password = "heslo";
            var expected = "Přihlášen";
            var sut = new LoginViewModel(new TestAuthentication())
            {
                LoginName = username,
                Password = password
            };
            sut.LoginCommand.Execute(null);
            Assert.Equal(expected, sut.Message);

        }
    }
}