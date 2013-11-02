using System.Windows;
using MsFest.ReactiveUI.Wpf.ViewModels;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IViewFor<LoginViewModel>
    {
        public MainWindow()
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

        //public static readonly DependencyProperty ViewModelProperty =
        //    DependencyProperty.Register("ViewModel", typeof(LoginViewModel), typeof(MainWindow), new PropertyMetadata(null));
        public LoginViewModel ViewModel { get; set; }
    }
}
