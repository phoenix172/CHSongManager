using System.Threading.Tasks;
using System.Windows.Input;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using TinyMVVM;

namespace CHSongManager.ViewModels
{
    internal class DownloadableSongViewModel : ViewModelBase, ISong
    {
        private readonly DownloadableSong _song;
        private readonly IDialogService _dialogService;

        public DownloadableSongViewModel(DownloadableSong song, IDialogService dialogService)
        {
            _song = song;
            _dialogService = dialogService;
            Download = new RelayCommand(async ()=>await DownloadAsync());
        }

        public ICommand Download { get; }

        public string Artist => _song.Artist;
        public string Name => _song.Name;
        public string Album => _song.Album;
        public bool IsSelected { get; set; }

        public async Task DownloadAsync(bool suppressError = false)
        {
            bool result = await _song.DownloadAsync();
            if(!result && !suppressError)
                _dialogService.ShowError("Song has already been downloaded!");
        }
    }
}