using System.Windows.Input;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using TinyMVVM;

namespace CHSongManager.ViewModels
{
    internal class DownloadableSongViewModel : ISong
    {
        private readonly DownloadableSong _song;

        public DownloadableSongViewModel(DownloadableSong song)
        {
            _song = song;
            Download = new RelayCommand(async () => await song.DownloadAsync());
        }

        public ICommand Download { get; }

        public string Artist => _song.Artist;
        public string Name => _song.Name;
        public string Album => _song.Album;
    }
}