using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using TinyMVVM;

namespace CHSongManager.Helpers
{
    public interface ISongMapper
    {
        IEnumerable<ISong> Map(IEnumerable<ISong> source);
    }

    public class SongMapper : ISongMapper
    {
        private readonly IDialogService _dialogService;
        private readonly ISongDownloader _downloader;

        public SongMapper(IDialogService dialogService, ISongDownloader downloader)
        {
            _dialogService = dialogService;
            _downloader = downloader;
        }

        public IEnumerable<ISong> Map(IEnumerable<ISong> source)
        {
            return source.Select(Map);
        }

        private ISong Map(ISong song)
        {
            switch (song)
            {
                case DownloadableSong downloadableSong:
                    return new DownloadableSongViewModel(downloadableSong, _dialogService, _downloader);
                case LocalSong localSong:
                    return new LocalSongViewModel(localSong);
                case DownloadTask localSong:
                    return new DownloadTaskViewModel(localSong);
                default: return song;
            }
        }
    }
}
