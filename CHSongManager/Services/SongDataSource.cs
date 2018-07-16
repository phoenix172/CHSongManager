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
        private readonly ISongMapper _songMapper;

        public SongDataSource(ISongMapper songMapper, ProviderManager providerManager)
        {
            _songMapper = songMapper;
            Criteria = SearchCriteria.Empty;
            Providers = providerManager;
            Providers.CurrentProviderChanged += async (s, e) =>
            {
                OnPropertyChanged(nameof(CurrentProvider));
                await LoadAsync();
            };
        }

        public SearchCriteria Criteria { get; }
        public ICollectionView Songs { get; private set; }
        public bool IsLoading { get; private set; }
        public bool IsRemoteSearch => false;

        public ProviderManager Providers { get; }

        public ISongProvider CurrentProvider
        {
            get => Providers.Current;
            set
            {
                Guard.NotNull(value, nameof(CurrentProvider));
                Providers.Current = value;
            }
        }

        public async Task LoadAsync()
        {
            try
            {
                IsLoading = true;
                var songs = await CurrentProvider.GetAsync(Criteria);
                await Task.Run(() =>
                    Songs = CollectionViewSource.GetDefaultView(_songMapper.Map(songs)));
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            Providers.ApplyConfiguration(options);
        }
    }
}