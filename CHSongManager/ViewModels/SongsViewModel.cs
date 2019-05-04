using System;
using System.Threading.Tasks;
using System.Windows.Input;
using CHSongManager.Helpers;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Commands;
using TinyMVVM.Utilities;

namespace CHSongManager.ViewModels
{
    public class SongsViewModel : ViewModelBase, ISongsViewModel
    {
        private readonly ISongDataSource _songDataSource;
        private readonly ProviderMapper _providerMapper;
        private IProviderViewModel _providerViewModel;

        public SongsViewModel()
            : this(new MockSongDataSource(), new SearchViewModel(),
                new SongListViewModel(), new SelectProviderViewModel(),
                new ProviderMapper())
        {
            ThrowIfNotInDesignMode();
        }

        [Inject]
        public SongsViewModel(
            ISongDataSource songDataSource,
            ISearchViewModel searchViewModel,
            ISongListViewModel songListViewModel,
            ISelectProviderViewModel selectProviderViewModel,
            ProviderMapper providerMapper)
        {
            Guard.NotNull(providerMapper, nameof(providerMapper));
            Guard.NotNull(songDataSource, nameof(songDataSource));
            Guard.NotNull(searchViewModel, nameof(searchViewModel));
            Guard.NotNull(songListViewModel, nameof(songListViewModel));
            Guard.NotNull(selectProviderViewModel, nameof(selectProviderViewModel));

            _songDataSource = songDataSource;
            _providerMapper = providerMapper;
            SearchVM = searchViewModel;
            SongListVM = songListViewModel;
            SelectProviderVM = selectProviderViewModel;

            LoadedCommand = new RelayCommand(async ()=>await LoadedAsync());

            PropagateChanges(_songDataSource, nameof(ISongDataSource.CurrentProvider), nameof(ProviderVM));
        }

        private async Task LoadedAsync()
        {
            await SelectProviderVM.LoadAsync();
        }

        public ISearchViewModel SearchVM { get; }
        public ISongListViewModel SongListVM { get; }
        public ISelectProviderViewModel SelectProviderVM { get; }
        public IProviderViewModel ProviderVM => GetProviderViewModel();

        public ICommand LoadedCommand { get; }

        private IProviderViewModel GetProviderViewModel()
        {
            if (_providerViewModel?.SongProvider != _songDataSource.CurrentProvider)
            {
                _providerViewModel = _providerMapper.Map(_songDataSource.CurrentProvider);
            }

            return _providerViewModel;
        }
    }

    public interface IProviderViewModel
    {
        ISongProvider SongProvider { get; }
    }
}