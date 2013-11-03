using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MsFest.ReactiveUI.Wpf.ViewModels;
using ReactiveUI;

namespace MsFest.ReactiveUI.WinForm
{
    public partial class FormMain : Form, IViewFor<LoginViewModel>
    {
        public FormMain()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
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
