using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class SongDownloader : ISongDownloader
    {
        private string _downloadPath;
        private ChorusLib.SongDownloader _songDownloader;

        public SongDownloader(IConfigurationOptions options)
        {
            ApplyConfiguration(options);
        }

        public async Task DownloadAsync(Song song)
        {
            await _songDownloader.DownloadAsync(song);
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            _downloadPath = options.SongFolder;
            _songDownloader = new ChorusLib.SongDownloader(_downloadPath);
        }
    }
}