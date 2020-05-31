using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace RunViewer.Helpers
{
    public sealed class WpfDispatcherContext : IContext
    {
        private readonly Dispatcher _dispatcher;


        public WpfDispatcherContext()
            : this(Dispatcher.CurrentDispatcher) { }

        public WpfDispatcherContext(Dispatcher dispatcher)
        {
            this._dispatcher = dispatcher;
        }


        public void Invoke(Action action)
        {
            this._dispatcher.Invoke(action);
        }

        public void BeginInvoke(Action action)
        {
            this._dispatcher.BeginInvoke(action);
        }
    }
}
