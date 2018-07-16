using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using TinyMVVM.Interfaces;

namespace TinyMVVM
{
    public class WindowManager : IWindowManager
    {
        private readonly ConcurrentBag<Window> _windows = new ConcurrentBag<Window>();
        public IReadOnlyCollection<Window> Windows => (IReadOnlyCollection<Window>)_windows;

        public WindowManager()
        {
        }

        public void Show<TViewModel>(TViewModel viewModel) 
            where TViewModel : class
        {
            var window = CreateWindow(viewModel);
            window.Show();
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = CreateDialogWindow(viewModel);
            return window.ShowDialog();
        }

        public void CloseWindow<TViewModel>(TViewModel viewModel, bool? dialogResult = null)
            where TViewModel : class
        {
            var window = Windows.FirstOrDefault(x => (x.DataContext as TViewModel)==viewModel);
            window.DialogResult = dialogResult;
            window.Close();
        }

        private Window CreateDialogWindow<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = CreateWindow(viewModel);
            window.ResizeMode = ResizeMode.NoResize;
            return window;
        }

        private Window CreateWindow<TViewModel>(TViewModel viewModel)
            where TViewModel : class
        {
            var window = new Window
            {
                DataContext = viewModel,
                Content = viewModel,
                SizeToContent = SizeToContent.WidthAndHeight
            };
            
            if (viewModel is ICanClose closable)
                closable.CloseRequested += (sender, dialogResult) 
                    => CloseWindow(viewModel, dialogResult);

            WindowCreated?.Invoke(this, window);

            _windows.Add(window);
            return window;
        }

        public event EventHandler<Window> WindowCreated;
        
    }
}