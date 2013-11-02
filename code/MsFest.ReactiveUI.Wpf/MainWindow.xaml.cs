using MsFest.ReactiveUI.Wpf.ViewModels;

namespace MsFest.ReactiveUI.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var appBootstrap = new AppBootstrap();
            DataContext = appBootstrap;
        }
    }
}
