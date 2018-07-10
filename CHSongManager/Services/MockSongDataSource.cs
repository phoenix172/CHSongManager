using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class MockSongDataSource : ISongDataSource
    {
        public SearchCriteria SearchCriteria { get; } = new SearchCriteria();
        public event PropertyChangedEventHandler PropertyChanged;
        public ISongProvider SongProvider { get; set; }

        public bool IsRemoteSearch => false;

        public bool IsLoading { get; set; } = true;
        public Task LoadAsync(SearchCriteria criteria)
        {
            return Task.CompletedTask;
        }

        public ICollectionView Songs { get; }
            = CollectionViewSource.GetDefaultView(GetSongs());

        private static IEnumerable<ISong> GetSongs()
        {
            yield return new SimpleSong("Pesen1", "Gosho", "Album2");
            yield return new SimpleSong("Pesen2", "Pesho", "Album3");
            yield return new SimpleSong("Pesen3", "Stamat", "Album1");
        }

        public void ApplyConfiguration(IConfigurationOptions options) { }
    }
}