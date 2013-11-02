using MsFest.ReactiveUI.Wpf.Views;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.ViewModels
{
    public class AppBootstrap : ReactiveObject, IScreen
    {
        public AppBootstrap()
        {
            Router = new RoutingState();
            var resolver = RxApp.MutableResolver;
            resolver.RegisterConstant(this, typeof(IScreen));
            resolver.Register(() => new LoginRouteView(), typeof(IViewFor<LoginRouteViewModel>));
            resolver.Register(() => new PersonListView(), typeof(IViewFor<PersonListViewModel>));

            Router.Navigate.Execute(new LoginRouteViewModel(this));
        }

        public IRoutingState Router { get; private set; }
    }
}