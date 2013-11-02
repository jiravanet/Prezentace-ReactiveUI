using MsFest.ReactiveUI.Wpf.ViewModels;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : IViewFor<LoginViewModel>
    {
        public LoginView()
        {
            ViewModel = new LoginViewModel();
            InitializeComponent();
            this.Bind(ViewModel,
                model => model.LoginName,
                window => window.userName.Text);
            this.Bind(ViewModel,
                model => model.Password,
                window => window.password.Text);
            this.OneWayBind(ViewModel,
                model => model.Message,
                window => window.message.Text);
            this.BindCommand(ViewModel,
                model => model.LoginCommand,
                window => window.login);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginViewModel)value; }
        }
        public LoginViewModel ViewModel { get; set; }

    }
}
