using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Extensions;
using TinyMVVM.Interfaces;

namespace CHSongManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IKernel _kernel;
        private IWindowManager _windowManager;

        public App()
        {
            InitializeComponent();
            _kernel = ConfigureIoC();
            _windowManager = Resolve<IWindowManager>();
        }

        private IKernel ConfigureIoC()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDialogService>().To<DialogService>().InSingletonScope();
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IConfigurationOptions>().To<ConfigurationOptions>().InSingletonScope();

            kernel.Bind<DownloadManager>().ToSelf().InSingletonScope();
            kernel.Bind<ISongDownloader>().ToMethod(c => c.Kernel.Get<DownloadManager>());

            kernel.Bind<ISongDataSource>().To<SongDataSource>().InSingletonScope();

            kernel.Bind<IMainViewModel>().To<MainViewModel>();
            kernel.Bind<ISongListViewModel>().To<SongListViewModel>();
            kernel.Bind<IConfigurationViewModel>().To<ConfigurationViewModel>();
            kernel.Bind<IDownloadViewModel>().To<DownloadViewModel>();
            kernel.Bind<ISongsViewModel>().To<SongsViewModel>();
            kernel.Bind<ISearchViewModel>().To<SearchViewModel>();
            kernel.Bind<ISelectProviderViewModel>().To<SelectProviderViewModel>();

            kernel.Bind<ISongProvider>().To<LocalSongProvider>();
            kernel.Bind<ISongProvider>().To<ChorusSongProvider>();
            kernel.Bind<ISongProvider>().ToMethod(c => c.Kernel.Get<DownloadManager>());

            return kernel;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            var mainViewModel = Resolve<IMainViewModel>();
            _windowManager.ConfigureMaximized<IMainViewModel>();
            _windowManager.Show(mainViewModel);
        }

        private T Resolve<T>()
            where T : class
        {
            return _kernel.Get<T>();
        }
    }
}
