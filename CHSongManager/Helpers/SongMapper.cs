using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.ViewModels;

namespace CHSongManager.Helpers
{
    public static class SongMapper
    {
        public static IEnumerable<ISong> Map(IEnumerable<ISong> source)
        {
            return source.Select(MapSong);
        }

        private static ISong MapSong(ISong song)
        {
            switch (song)
            {
                case DownloadableSong downloadableSong:
                    return new DownloadableSongViewModel(downloadableSong);
                case LocalSong localSong:
                    return new LocalSongViewModel(localSong);
                case DownloadTask localSong:
                    return new DownloadTaskViewModel(localSong);
                default: return song;
            }
        }
    }
}
