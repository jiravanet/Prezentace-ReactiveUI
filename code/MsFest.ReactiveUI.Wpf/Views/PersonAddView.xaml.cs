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
    /// Interaction logic for PersonAddView.xaml
    /// </summary>
    public partial class PersonAddView : IViewFor<PersonAddViewModel>
    {
        public PersonAddView()
        {
            InitializeComponent();
            this.WhenNavigatedTo(ViewModel,
                () =>
                {
                    DataContext = ViewModel;
                    return Disposable.Empty;
                });
        }

        public static readonly DependencyProperty ViewModelProperty =
DependencyProperty.Register("ViewModel", typeof(PersonAddViewModel), typeof(PersonAddView), new PropertyMetadata(null));

        object IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (PersonAddViewModel)value; }
        }

        public PersonAddViewModel ViewModel
        {
            get
            {
                return (PersonAddViewModel)GetValue(ViewModelProperty);
            }
            set
            {
                SetValue(ViewModelProperty,
                    value);
            }
        }
    }
}
