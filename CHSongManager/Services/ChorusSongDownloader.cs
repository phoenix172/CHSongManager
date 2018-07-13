using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class ChorusSongDownloader
    {
        private string _downloadPath;
        private ChorusLib.SongDownloader _songDownloader;

        public ChorusSongDownloader(IConfigurationOptions options)
        {
            ApplyConfiguration(options);
        }

        public async Task<bool> DownloadAsync(Song song)
        {
            await _songDownloader.DownloadAsync(song);
            return true;
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            _downloadPath = options.SongFolder;
            _songDownloader = new ChorusLib.SongDownloader(_downloadPath);
        }
    }
}