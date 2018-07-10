using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Interfaces;
using TinyMVVM.Utilities;

namespace CHSongManager.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private int _selectedVmIndex;

        public MainViewModel()
            : this(new ConfigurationViewModel(), new SongsViewModel())
        {
            ThrowIfNotInDesignMode();
        }

        [Inject]
        public MainViewModel(IConfigurationViewModel configurationViewModel,
            ISongsViewModel songsViewModel)
        {
            Guard.NotNull(songsViewModel, nameof(songsViewModel));
            Guard.NotNull(configurationViewModel, nameof(configurationViewModel));

            SongsVM = songsViewModel;
            ConfigurationVM = configurationViewModel;
            ConfigurationVM.CloseRequested += (s, e) => SelectedVMIndex = 0;
        }

        public ISongsViewModel SongsVM { get; }
        public IConfigurationViewModel ConfigurationVM { get; }

        public int SelectedVMIndex
        {
            get { return _selectedVmIndex; }
            set
            {
                if (value == _selectedVmIndex) return;
                _selectedVmIndex = value;
                OnPropertyChanged();
            }
        }
    }
}
