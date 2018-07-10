using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TinyMVVM.Interfaces;

namespace TinyMVVM
{
    public abstract class ViewModelBase : ObservableObject
    {
        public bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());

        protected void ThrowIfNotInDesignMode()
        {
            if(!IsInDesignMode)
                throw new InvalidOperationException("This code is meant to be executed in design mode only");
        }
    }
}
