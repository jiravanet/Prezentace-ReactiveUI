using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows.Forms;
using ReactiveUI;

namespace MsFest.ReactiveUI.WinForm
{
    public class CommandToWinformsButton : ICreatesCommandBinding
    {
        public int GetAffinityForObject(Type type, bool hasEventTarget)
        {
            if (type != typeof(Button)) return 0;
            if (hasEventTarget) return 0;
            return 10;
        }

        public IDisposable BindCommandToObject<TEventArgs>(System.Windows.Input.ICommand command, object target, IObservable<object> commandParameter, string eventName)
        {
            throw new NotImplementedException();
        }

        public IDisposable BindCommandToObject(System.Windows.Input.ICommand command, object target, IObservable<object> commandParameter)
        {
            var button = target as Button;

            var clicked = Observable.FromEventPattern<EventHandler, EventArgs>(x => button.Click += x, x => button.Click -= x);
            var canExecute = Observable.FromEventPattern<EventHandler, EventArgs>(x => command.CanExecuteChanged += x, x => command.CanExecuteChanged -= x);

            return new CompositeDisposable(
                clicked.InvokeCommand(command),
                canExecute
                    .Select(_ => command.CanExecute(null))
                    .StartWith(command.CanExecute(null))
                    .Subscribe(x => button.Enabled = x));
        }
    }
}