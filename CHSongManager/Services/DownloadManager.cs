using System.Collections.Generic;
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
        private readonly ISongDownloader _downloader;

        public DownloadManager(IConfigurationOptions options)
        {
            _downloadTasks = new List<DownloadTask>();
            _downloader = new SongDownloader(options);
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

        public async Task DownloadAsync(Song song)
        {
            var downloadableSong = new DownloadableSong(song, _downloader);
            var task = new DownloadTask(downloadableSong);
            _downloadTasks.Add(task);
            await task.RunAsync();
        }
    }
}