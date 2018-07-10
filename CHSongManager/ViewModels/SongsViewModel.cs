using System;
using System.Windows.Input;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Utilities;

namespace CHSongManager.ViewModels
{
    public class SongsViewModel : ViewModelBase, ISongsViewModel
    {
        public SongsViewModel()
            : this(new SearchViewModel(), new SongListViewModel(), new SelectProviderViewModel())
        {
            ThrowIfNotInDesignMode();
        }

        [Inject]
        public SongsViewModel(
            ISearchViewModel searchViewModel,
            ISongListViewModel songListViewModel,
            ISelectProviderViewModel selectProviderViewModel)
        {
            Guard.NotNull(searchViewModel, nameof(searchViewModel));
            Guard.NotNull(songListViewModel, nameof(songListViewModel));
            Guard.NotNull(selectProviderViewModel, nameof(selectProviderViewModel));

            SearchVM = searchViewModel;
            SongListVM = songListViewModel;
            SelectProviderVM = selectProviderViewModel;

            LoadedCommand = new RelayCommand(Loaded);
        }

        private void Loaded()
        {
            SelectProviderVM.Load();
        }

        public ISearchViewModel SearchVM { get; }
        public ISongListViewModel SongListVM { get; }
        public ISelectProviderViewModel SelectProviderVM { get; }

        public ICommand LoadedCommand { get; }
    }
}