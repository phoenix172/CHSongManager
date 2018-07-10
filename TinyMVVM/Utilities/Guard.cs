using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinyMVVM.Utilities
{
    public static class Guard
    {
        [DebuggerHidden]
        public static void NotNull(object argument, string argumentName)
        {
            if(argument == null)
                throw new ArgumentNullException(argumentName);
        }
    }
}
