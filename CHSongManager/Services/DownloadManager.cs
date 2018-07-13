using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class DownloadManager : ISongProvider, ISongDownloader
    {
        private readonly List<DownloadTask> _downloadTasks;
        private readonly ChorusSongDownloader _downloader;

        public DownloadManager(IConfigurationOptions options)
        {
            _downloadTasks = new List<DownloadTask>();
            _downloader = new ChorusSongDownloader(options);
        }

        public string Name => "Downloading";

        public Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria)
        {
            return Task.FromResult<IEnumerable<ISong>>(_downloadTasks);
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            _downloader.ApplyConfiguration(options);
        }

        public async Task<bool> DownloadAsync(DownloadableSong song)
        {
            if (IsAlreadyDownloadingSong(song))
                return false;
            var task = new DownloadTask(song);
            _downloadTasks.Add(task);
            await task.RunAsync();
            return true;
        }

        private bool IsAlreadyDownloadingSong(DownloadableSong song)
        {
            return _downloadTasks.Any(x => x.Name == song.Name && x.Album == song.Album && x.Artist == song.Artist);
        }
    }
}