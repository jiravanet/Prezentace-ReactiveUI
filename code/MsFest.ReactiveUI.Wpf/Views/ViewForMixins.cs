using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace MsFest.ReactiveUI.Wpf.Views
{
    // XXX: Ignore the man behind this curtain. This will soon be in ReactiveUI itself
    public static class ViewForMixins
    {
        public static IDisposable WhenNavigatedTo<TView, TViewModel>(this TView This, TViewModel viewModel, Func<IDisposable> onNavigatedTo)
            where TView : IViewFor<TViewModel>
            where TViewModel : class, IRoutableViewModel
        {
            var disp = Disposable.Empty;
            var inner = This.WhenAny(x => x.ViewModel, x => x.Value)
                .Where(x => x != null && x.HostScreen.Router.GetCurrentViewModel() == x)
                .Subscribe(x =>
                {
                    if (disp != null) disp.Dispose();
                    disp = onNavigatedTo();
                });

            return Disposable.Create(() =>
            {
                inner.Dispose();
                disp.Dispose();
            });
        }
    }
}