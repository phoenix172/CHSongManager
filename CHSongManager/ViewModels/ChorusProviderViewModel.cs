using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using TinyMVVM;
using TinyMVVM.Commands;

namespace CHSongManager.ViewModels
{
    public class ChorusProviderViewModel : ViewModelBase, IProviderViewModel
    {
        private readonly ISongDataSource _songDataSource;
        private readonly ISearchViewModel _searchViewModel;
        private readonly ChorusSongProvider _chorus;

        public ChorusProviderViewModel(ISongDataSource songDataSource, ChorusSongProvider provider, ISearchViewModel searchViewModel)
        {
            _songDataSource = songDataSource;
            _searchViewModel = searchViewModel;
            SongProvider = _chorus = provider;

            DownloadSelectedCommand = new RelayCommand(DownloadSelected);
            DownloadAllCommand = new RelayCommand(DownloadAll);
            NextCommand = new RelayCommand(async ()=> await NextAsync());   
            PrevCommand = new RelayCommand(async ()=> await PrevAsync());

            Page = _chorus.Page;
            _searchViewModel.Searching += (s,e) => _chorus.Page = Page = 1;
        }

        private async Task NextAsync()
        {
            _chorus.Page = ++Page;
            await _songDataSource.LoadAsync();
        }

        private async Task PrevAsync()
        {
            if (_chorus.Page == 1) return;
            _chorus.Page = --Page;
            await _songDataSource.LoadAsync();
        }

        private void DownloadSelected()
        {
            var songs = GetDownloadableSongs().Where(x => x.IsSelected).ToList();
            DownloadSongs(songs);
            foreach (var song in songs)
            {
                song.IsSelected = false;
            }
        }

        private void DownloadAll()
        {
            var songs = GetDownloadableSongs();
            DownloadSongs(songs);
        }

        private IEnumerable<DownloadableSongViewModel> GetDownloadableSongs()
        {
            return _songDataSource.Songs.OfType<DownloadableSongViewModel>();
        }

        private void DownloadSongs(IEnumerable<DownloadableSongViewModel> songs)
        {
            foreach (var song in songs)
            {
                song.DownloadAsync(true);
            }
        }

        public ISongProvider SongProvider { get; }
        public int Page { get; private set; }

        public ICommand NextCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand DownloadSelectedCommand { get; }
        public ICommand DownloadAllCommand { get; }
    }
}