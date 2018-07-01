using System.Collections.Generic;
using System.Windows;

namespace CHSongManager.Infrastructure.Interfaces
{
    public interface IWindowManager
    {
        IEnumerable<Window> Windows { get; }

        void Show<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel;

        void CloseWindow<TViewModel>(TViewModel viewModel, bool? dialogResult)
            where TViewModel : IViewModel;
    }
}