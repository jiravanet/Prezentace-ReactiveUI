using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for PersonListView.xaml
    /// </summary>
    public partial class PersonListView : UserControl, IViewFor<PersonListViewModel>
    {
        public PersonListView()
        {
            ViewModel = new PersonListViewModel();
            InitializeComponent();
            this.OneWayBind(ViewModel,
                model => model.Persons,
                window => window.persons.ItemsSource);
            this.BindCommand(ViewModel,
                model => model.RefreshCommand,
                window => window.refresh);
        }

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PersonListViewModel)value; }
        }

        public PersonListViewModel ViewModel { get; set; }
    }
}
