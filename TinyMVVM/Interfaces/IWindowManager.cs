using System;
using System.Collections.Generic;
using System.Windows;

namespace TinyMVVM.Interfaces
{
    public interface IWindowManager
    {
        IReadOnlyCollection<Window> Windows { get; }

        void Show<TViewModel>(TViewModel viewModel)
            where TViewModel : class;

        bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : class;

        void CloseWindow<TViewModel>(TViewModel viewModel, bool? dialogResult)
            where TViewModel : class;

        event EventHandler<Window> WindowCreated;
    }
}