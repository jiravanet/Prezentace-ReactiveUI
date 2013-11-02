using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MsFest.ReactiveUI.Wpf.ViewModels;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for LoginRouteView.xaml
    /// </summary>
    public partial class LoginRouteView : IViewFor<LoginRouteViewModel>
    {
        public LoginRouteView()
        {
            InitializeComponent();
            this.WhenNavigatedTo(ViewModel, () =>
            {
                DataContext = ViewModel;
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
                return Disposable.Empty;
            });
        }

        public static readonly DependencyProperty ViewModelProperty =
    DependencyProperty.Register("ViewModel", typeof(LoginRouteViewModel), typeof(LoginRouteView), new PropertyMetadata(null));


        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (LoginRouteViewModel)value; }
        }

        public LoginRouteViewModel ViewModel
        {
            get
            {
                return (LoginRouteViewModel)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty,
                    value);
            }
        }
    }
}
