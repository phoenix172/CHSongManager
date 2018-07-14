using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Models
{
    public class DownloadableSong : ISong
    {
        private readonly Song _chorusSong;
        private readonly ChorusSongDownloader _chorusDownloader;

        public DownloadableSong(Song chorusSong,
            ChorusSongDownloader chorusDownloader)
        {
            _chorusSong = chorusSong;
            _chorusDownloader = chorusDownloader;
        }

        public string Artist => _chorusSong.Artist;
        public string Name => _chorusSong.Name;
        public string Album => _chorusSong.Album;

        public Task<bool> DownloadAsync()
        {
            return _chorusDownloader.DownloadAsync(_chorusSong);
        }
    }
}