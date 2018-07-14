using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class ChorusSongProvider : ISongProvider, IConfigurable
    {
        private readonly ChorusApi _chorus;
        private readonly object _searchLock = new object();
        private readonly ChorusSongDownloader _downloader;

        public ChorusSongProvider(IConfigurationOptions options)
        {
            _chorus = new ChorusApi("chorus.fightthe.pw/api");
            _downloader = new ChorusSongDownloader(options);
        }

        public string Name => "Chorus";
        public int Page { get; set; } = 1;

        public async Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria)
        {
            var result = await SearchAsync(criteria);
            return result?.Select(MapSong)
                    ?? Enumerable.Empty<ISong>();
        }

        private async Task<List<Song>> SearchAsync(SearchCriteria criteria)
        {
            var filter = BuildFilter(criteria);
            return await _chorus.SearchAsync(filter, Page);
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            _downloader.ApplyConfiguration(options);
        }

        private ISong MapSong(Song song)
        {
            return new DownloadableSong(song, _downloader);
        }

        private SongProps BuildFilter(SearchCriteria criteria)
        {
            return new SongProps
            {
                Album = criteria.Album,
                Artist = criteria.Artist,
                Name = criteria.Name
            };
        }
    }
}