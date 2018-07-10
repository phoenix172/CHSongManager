using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyMVVM.Interfaces
{
    public interface ICanClose
    {
        event EventHandler<bool?> CloseRequested;
    }
}
