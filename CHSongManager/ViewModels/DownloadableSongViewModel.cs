using System.Windows.Input;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using TinyMVVM;

namespace CHSongManager.ViewModels
{
    internal class DownloadTaskViewModel : ViewModelBase, ISong
    {
        private readonly DownloadTask _song;

        public DownloadTaskViewModel(DownloadTask song)
        {
            _song = song;
            _song.Completed += (s,e) => OnPropertyChanged(nameof(IsCompleted));
        }
        
        public string Artist => _song.Artist;
        public string Name => _song.Name;
        public string Album => _song.Album;

        public bool IsCompleted => _song.IsCompleted;
    }
}