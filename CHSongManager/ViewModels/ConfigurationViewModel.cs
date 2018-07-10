using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using CHSongManager.Annotations;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Interfaces;

namespace CHSongManager.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase, IConfigurationViewModel
    {
        private readonly ISongDataSource _songDataSource;
        private readonly IDialogService _dialogService;

        public ConfigurationViewModel()
            : this(null, null, null)
        {
            ThrowIfNotInDesignMode();
        }

        [Inject]
        public ConfigurationViewModel(ISongDataSource songDataSource, IConfigurationOptions options, IDialogService dialogService)
        {
            _songDataSource = songDataSource;
            _dialogService = dialogService;
            Options = options;
            SelectFolderCommand = new RelayCommand<string>(SelectFolder);
            OKCommand = new RelayCommand(OK);
        }

        public ICommand SelectFolderCommand { get; }
        public ICommand OKCommand { get; }

        public IConfigurationOptions Options { get; }
        public event EventHandler<bool?> CloseRequested;

        public string SongFolder
        {
            get => Options.SongFolder;
            set => Options.SongFolder = value;
        }

        private void OK()
        {
            if (Options.HasValidConfiguration())
            {
                Options.Save();
                _songDataSource.ApplyConfiguration(Options);
                CloseRequested?.Invoke(this, true);
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
    }
}
