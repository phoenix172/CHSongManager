using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CHSongManager.Helpers;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;
using Log_Easy;
using Ninject;
using Ninject.Extensions.NamedScope;
using TinyMVVM;
using TinyMVVM.Extensions;
using TinyMVVM.Interfaces;
using TinyMVVM.Extensions;

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
            DeleteExistingLog();
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            _kernel = ConfigureIoC();
            _windowManager = Resolve<IWindowManager>();
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Log(e.Exception.ToString());
            MessageBox.Show($"An error occured: {e.Exception.Message}", "CHSongManager", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            e.Handled = true;
        }

        private void DeleteExistingLog()
        {
            try
            {
                if (File.Exists("log.txt"))
                    File.Delete("log.txt");
            }
            catch
            {

            }
        }

        private void Log(string message)
        {
            var logger = new clsLogger("log.txt", DateTime.Now.ToLongTimeString());
            logger.LogOutput(message);
            logger.closeFile();
        }

        private IKernel ConfigureIoC()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IDialogService>().To<DialogService>().InSingletonScope();
            kernel.Bind<IWindowManager>().To<WindowManager>().InSingletonScope();
            kernel.Bind<IConfigurationOptions>().To<ConfigurationOptions>().InSingletonScope();

            kernel.Bind<IMainViewModel>().To<MainViewModel>().DefinesNamedScope(nameof(App));
            kernel.Bind<IConfigurationViewModel>().To<ConfigurationViewModel>().InNamedScope(nameof(App));

            kernel.Bind<ISongsViewModel>().To<SongsViewModel>().InNamedScope(nameof(App));
            kernel.Bind<ISongDataSource>().To<SongDataSource>().InNamedScope(nameof(App));
            kernel.Bind<ISearchViewModel>().To<SearchViewModel>().InNamedScope(nameof(App));
            kernel.Bind<ISelectProviderViewModel>().To<SelectProviderViewModel>().InNamedScope(nameof(App));
            kernel.Bind<ISongListViewModel>().To<SongListViewModel>().InNamedScope(nameof(App));

            kernel.Bind<DownloadManager>().ToSelf();
            var downloadManager = kernel.Get<DownloadManager>();
            kernel.Bind<ISongDownloader>().ToConstant(downloadManager).InNamedScope(nameof(App));

            kernel.Bind<ISongProvider>().To<LocalSongProvider>();
            kernel.Bind<ISongProvider>().To<ChorusSongProvider>();
            kernel.Bind<ISongProvider>().ToConstant(downloadManager).InNamedScope(nameof(App));

            kernel.Bind<ISongMapper>().To<SongMapper>();

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
            _windowManager.Configure<IMainViewModel>(window =>
            {
                window.SizeToContent = SizeToContent.Manual;
                window.WindowState = WindowState.Maximized;
            });
            _windowManager.Show(mainViewModel);
        }

        private T Resolve<T>()
            where T : class
        {
            return _kernel.Get<T>();
        }
    }
}
