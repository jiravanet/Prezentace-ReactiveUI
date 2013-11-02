using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Controls;
using MsFest.ReactiveUI.Wpf.ViewModels;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.Views
{
    /// <summary>
    /// Interaction logic for PersonListView.xaml
    /// </summary>
    public partial class PersonListView : IViewFor<PersonListViewModel>
    {
        public PersonListView()
        {
            
            InitializeComponent();
            this.WhenNavigatedTo(ViewModel,
                () =>
                {
                    DataContext = ViewModel;
                    this.OneWayBind(ViewModel,
                        model => model.Persons,
                        window => window.persons.ItemsSource);
                    this.BindCommand(ViewModel,
                        model => model.RefreshCommand,
                        window => window.refresh);
                    return Disposable.Empty;
                });
        }

        public static readonly DependencyProperty ViewModelProperty =
DependencyProperty.Register("ViewModel", typeof(PersonListViewModel), typeof(PersonListView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PersonListViewModel)value; }
        }

        public PersonListViewModel ViewModel
        {
            get
            {
                return (PersonListViewModel)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty,
                    value);
            }
        }
    }
}
