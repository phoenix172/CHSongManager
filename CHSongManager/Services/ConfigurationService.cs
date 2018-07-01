using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CHSongManager.Infrastructure;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;

namespace CHSongManager
{
    public class ConfigurationService : IConfigurationService
    { 
        private readonly IWindowManager _windowManager;
        private readonly IDialogService _dialogService;

        public ConfigurationService(IWindowManager windowManager, IDialogService dialogService)
        {
            _windowManager = windowManager;
            _dialogService = dialogService;
            Options = new ConfigurationOptions();
        }

        public IConfigurationOptions Options { get; set; }

        public bool Configure(bool force = false)
        {
            if (!ShouldConfigure(force)) return true;
            return ShowConfigurationDialog();
        }

        private bool ShowConfigurationDialog()
        {
            IConfigurationViewModel configurationViewModel = 
                new ConfigurationViewModel(Options, _windowManager, _dialogService);
            bool dialogResult = _windowManager.ShowDialog(configurationViewModel) ?? false;
            if(dialogResult)
                Options.Save();
            return dialogResult;
        }

        private bool ShouldConfigure(bool force)
        {
            return !Options.HasValidConfiguration() || force;
        }
    }
}
