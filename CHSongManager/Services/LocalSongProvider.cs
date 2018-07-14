using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CHSongManager.Services
{
    public class LocalSongProvider : ISongProvider, IConfigurable
    {
        private string _songFolder;
        private const string MetadataFileName = "song.ini";

        public LocalSongProvider(IConfigurationOptions options)
        {
            ApplyConfiguration(options);
        }

        public string Name => "Local";

        public Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria = null)
        {
            return Task.Run(()=>Get(criteria??SearchCriteria.Empty));
        }

        private IEnumerable<ISong> Get(SearchCriteria criteria)
        {
            try
            {
                var songMetadataFiles = Directory.GetFiles(_songFolder, "song.ini", SearchOption.AllDirectories).ToList();
                return songMetadataFiles.Select(x =>
                {
                    LocalSong.TryParseFromMetadataFile(x, out LocalSong song);
                    return song;
                }).Where(x => x != null)
                    .Where(SearchPredicate(criteria));
            }
            catch (Exception e)
            {
                throw new SongScanException(e);
            }
        }

        private Func<ISong, bool> SearchPredicate(SearchCriteria criteria)
        {
            return song => (song.Name?.Contains(criteria.Name ?? "") ?? false) &&
                             (song.Album?.Contains(criteria.Album ?? "") ?? false) &&
                             (song.Artist?.Contains(criteria.Artist ?? "") ?? false);
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            _songFolder = options.SongFolder;
        }
    }

    public class SongScanException : Exception
    {
        public SongScanException(Exception innerException)
            : base("An error occured while loading songs. See inner exception for details.", innerException)
        {
        }
    }
}
