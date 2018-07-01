using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using CHSongManager.Annotations;
using CHSongManager.Infrastructure;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;

namespace CHSongManager.ViewModels
{
    public class ConfigurationViewModel : IConfigurationViewModel
    {
        private readonly IConfigurationOptions _options;
        private readonly IWindowManager _windowManager;
        private readonly IDialogService _dialogService;

        public ConfigurationViewModel(IConfigurationOptions options, 
            IWindowManager windowManager, IDialogService dialogService)
        {
            _options = options;
            _windowManager = windowManager;
            _dialogService = dialogService;
            SelectFolderCommand = new RelayCommand<string>(SelectFolder);
            OKCommand = new RelayCommand(OK);
        }

        public ICommand SelectFolderCommand { get; }
        public ICommand OKCommand { get; }

        public string SongFolder
        {
            get { return _options.SongFolder; }
            set
            {
                if (value == _options.SongFolder) return;
                _options.SongFolder = value;
                OnPropertyChanged();
            }
        }

        private void OK()
        {
            if (_options.HasValidConfiguration())
            {
                _windowManager.CloseWindow(this, true);
            }
            else
            {
                _dialogService.ShowError("Invalid path");
            }
        }

        private void SelectFolder(string currentFolder)
        {
            if (_dialogService.ShowFolderBrowser(currentFolder, out string songFolder))
                SongFolder = songFolder;
        }

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
