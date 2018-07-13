using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Models
{
    public class DownloadableSong : ISong
    {
        private readonly Song _chorusSong;
        private readonly ISongDownloader _downloader;

        public DownloadableSong(Song chorusSong, ISongDownloader downloader)
        {
            _chorusSong = chorusSong;
            _downloader = downloader;
        }

        public string Artist => _chorusSong.Artist;
        public string Name => _chorusSong.Name;
        public string Album => _chorusSong.Album;

        public Task<bool> DownloadAsync()
        {
            return _downloader.DownloadAsync(this);
        }
    }
}