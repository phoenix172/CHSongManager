using System.IO;
using System.Windows.Input;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using TinyMVVM;

namespace CHSongManager.ViewModels
{
    internal class LocalSongViewModel : ISong
    {
        private readonly LocalSong _song;

        public LocalSongViewModel(LocalSong song)
        {
            _song = song;
            Location = Path.GetDirectoryName(song.MetadataLocation);
        }

        public string Location { get; }
        public string Artist => _song.Artist;
        public string Name => _song.Name;
        public string Album => _song.Album;
    }
}