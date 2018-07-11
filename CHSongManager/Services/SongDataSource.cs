using System;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;
using CHSongManager.Helpers;
using TinyMVVM;
using TinyMVVM.Utilities;

namespace CHSongManager.Services
{
    public class SongDataSource : ObservableObject, ISongDataSource
    {
        private ISongProvider _songProvider;

        public SongDataSource()
        {
            Criteria = SearchCriteria.Empty;
        }

        public SearchCriteria Criteria { get; }
        public ICollectionView Songs { get; private set; }
        public bool IsLoading { get; private set; }
        public bool IsRemoteSearch => false;

        public ISongProvider SongProvider
        {
            get => _songProvider;
            set
            {
                Guard.NotNull(value, nameof(SongProvider));
                _songProvider = value;
            }
        }

        public async Task LoadAsync()
        {
            IsLoading = true;
            var songs = await SongProvider.GetAsync(Criteria);
            await Task.Run(()=> Songs = CollectionViewSource.GetDefaultView(SongMapper.Map(songs)));
            IsLoading = false;
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            (_songProvider as IConfigurable)?.ApplyConfiguration(options);
        }
    }
}