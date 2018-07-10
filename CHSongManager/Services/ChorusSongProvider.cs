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
        private readonly ISongDownloader _downloader;
        private IEnumerable<Song> _lastResult;
        private readonly object _searchLock = new object();

        public ChorusSongProvider(ISongDownloader downloader)
        {
            _chorus = new ChorusApi("chorus.fightthe.pw/api");
            _downloader = downloader;
        }

        public string Name => "Chorus";

        public async Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria)
        {
            return await Task.Run(() =>
            {
                var result = Search(criteria);
                return result?.Select(MapSong)
                        ?? Enumerable.Empty<ISong>();
            });
        }

        private List<Song> Search(SearchCriteria criteria)
        {
            var filter = BuildFilter(criteria);
            return _chorus.Search(filter);
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