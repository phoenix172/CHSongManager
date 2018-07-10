using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Models.Interfaces;

namespace CHSongManager.Models
{
    public class LocalSong : ISong
    {
        private const string ArtistKey = "artist";
        private const string NameKey = "name";
        private const string AlbumKey = "album";

        public LocalSong(string metadataLocation, IDictionary<string, string> metadata)
        {
            Metadata = new ReadOnlyDictionary<string, string>(metadata);
            MetadataLocation = metadataLocation;
        }

        public string Artist => GetMetadataValue(ArtistKey);

        public string Name => GetMetadataValue(NameKey);

        public string Album => GetMetadataValue(AlbumKey);

        public string MetadataLocation { get; }

        public IReadOnlyDictionary<string,string> Metadata { get; }

        public static bool TryParseFromMetadataFile(string metadataLocation, out LocalSong localSong)
        {
            localSong = null;
            if (!File.Exists(metadataLocation)) return false;
            try
            {
                string[] metadataLines = File.ReadAllLines(metadataLocation);
                var songMetadata = metadataLines.Select(x => x.Split(new[] { "=" }, StringSplitOptions.RemoveEmptyEntries))
                    .Where(x => x.Length == 2)
                    .ToDictionary(x => x[0].Trim(), x => x[1].Trim());
                localSong = new LocalSong(metadataLocation, songMetadata);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private string GetMetadataValue(string key)
        {
            Metadata.TryGetValue(key, out string value);
            return value;
        }
    }
}
