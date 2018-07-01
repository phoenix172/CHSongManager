using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;

namespace CHSongManager.Infrastructure
{
    public class WindowManager : IWindowManager
    {
        private static IReadOnlyCollection<WindowMapping> _mappings;
        private readonly WindowCollection _windows;

        static WindowManager()
        {
            _mappings = GetMappings();
        }

        public WindowManager(WindowCollection windows)
        {
            _windows = windows;
        }

        public IEnumerable<Window> Windows => _windows.Cast<Window>();

        public void Show<TDataContext>(TDataContext viewModel) 
            where TDataContext : IViewModel
        {
            var window = ConstructWindowForViewModel(viewModel);
            window.Show();
        }

        public bool? ShowDialog<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var window = ConstructWindowForViewModel(viewModel);
            return window.ShowDialog();
        }

        public void CloseWindow<TViewModel>(TViewModel viewModel, bool? dialogResult = null)
            where TViewModel : IViewModel
        {
            var window = Windows.FirstOrDefault(x => x.DataContext.Equals(viewModel));
            window.DialogResult = dialogResult;
            window.Close();
        }

        private Window ConstructWindowForViewModel<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            var windowType = GetWindowType(viewModel);
            var window = Activator.CreateInstance(windowType) as WindowView;
            window.ViewModel = viewModel;
            return window;
        }

        private static Type GetWindowType<TViewModel>(TViewModel viewModel)
            where TViewModel : IViewModel
        {
            return _mappings.Single(x =>
                x.DataContextType.IsInstanceOfType(viewModel)).WindowType;
        }

        private static IReadOnlyCollection<WindowMapping> GetMappings()
        {
            return new List<WindowMapping>
            {
                Map<IConfigurationViewModel, ConfigurationWindow>(),
                Map<ISongListViewModel, SongListWindow>()
            }.AsReadOnly();

            WindowMapping Map<TViewModel, TWindow>()
                where TWindow : WindowView
                where TViewModel : class => WindowMapping.Map<TViewModel, TWindow>();
        }

        private class WindowMapping
        {
            private WindowMapping(Type dataContextType, Type windowType)
            {
                DataContextType = dataContextType;
                WindowType = windowType;
            }

            public Type DataContextType { get; }
            public Type WindowType { get; }

            public static WindowMapping Map<TViewModel, TWindow>()
                where TWindow : WindowView
                where TViewModel : class
            {
                return new WindowMapping(typeof(TViewModel), typeof(TWindow));
            }
        }
    }
}