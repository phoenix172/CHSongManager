using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CHSongManager.Infrastructure;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;

namespace CHSongManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IConfigurationService _configurationService;
        private readonly IWindowManager _windowManager;
        private readonly DialogService _dialogService;

        public App()
        {
            _windowManager = new WindowManager(Windows);
            _dialogService = new DialogService();
            _configurationService = new ConfigurationService(_windowManager, _dialogService);
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            if (Configure())
                ShowMainWindow();
            else
                Shutdown();
        }

        private bool Configure()
        {
            return _configurationService.Configure();
        }

        private void ShowMainWindow()
        {
            _windowManager.Show(new SongListViewModel());
            ShutdownMode = ShutdownMode.OnMainWindowClose;
        }
    }
}
