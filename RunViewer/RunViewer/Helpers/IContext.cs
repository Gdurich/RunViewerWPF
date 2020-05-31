using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunViewer.Helpers
{
    public interface IContext
    {
        void Invoke(Action action);
        void BeginInvoke(Action action);
    }
}
