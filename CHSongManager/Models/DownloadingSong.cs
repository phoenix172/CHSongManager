using System;
using System.Threading.Tasks;
using CHSongManager.Models.Interfaces;

namespace CHSongManager.Models
{
    public class DownloadTask : ISong
    {
        private readonly DownloadableSong _song;

        public DownloadTask(DownloadableSong song)
        {
            _song = song;
        }

        public async Task RunAsync()
        {
            await _song.DownloadAsync();
            IsCompleted = true;
            OnCompleted();
        }

        public string Artist => _song.Artist;

        public string Name => _song.Name;

        public string Album => _song.Album;

        public bool IsCompleted { get; private set; }

        public event EventHandler Completed;

        protected virtual void OnCompleted()
        {
            Completed?.Invoke(this, EventArgs.Empty);
        }
    }
}