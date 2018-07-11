using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace TinyMVVM
{
    public class ObservableObject : INotifyPropertyChanged
    {
        private bool _suppressPropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName]string property = null)
        {
            if(!_suppressPropertyChanged)
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        protected void PropagateChanges<TSource>(TSource sourceObject, string sourceProperty,
            string targetProperty = null)
        where TSource : INotifyPropertyChanged
        {
            sourceObject.PropertyChanged += (s, e) =>
            {
                if(e.PropertyName == sourceProperty)
                    OnPropertyChanged(targetProperty ?? sourceProperty);
            };
        }

        protected void SuppressPropertyChanged(Action action)
        {
            _suppressPropertyChanged = true;
            action();
            _suppressPropertyChanged = false;
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
