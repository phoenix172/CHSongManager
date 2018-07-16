using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using TinyMVVM;

namespace CHSongManager.ViewModels
{
    public class SelectProviderViewModel : ViewModelBase, ISelectProviderViewModel
    {
        private readonly ISongDataSource _songDataSource;
        private readonly IEnumerable<ISongProvider> _providers;

        public SelectProviderViewModel()
            : this(new MockSongDataSource(), SongProvider.MockProviders())
        {
            ThrowIfNotInDesignMode();
            LoadAsync();
        }

        public SelectProviderViewModel(ISongDataSource songDataSource, IEnumerable<ISongProvider> providers)
        {
            _songDataSource = songDataSource;
            _providers = providers;
        }

        public ICollectionView Providers { get; set; }

        public async Task LoadAsync()
        {
            Providers = CollectionViewSource.GetDefaultView(_providers);
            Providers.CurrentChanged += async (s,e)=>await CurrentProviderChangedAsync();
            await CurrentProviderChangedAsync();
        }

        private async Task CurrentProviderChangedAsync()
        {
            _songDataSource.SongProvider = Providers.CurrentItem as ISongProvider;
            await _songDataSource.LoadAsync();
        }
    }

    public interface ISelectProviderViewModel
    {
        ICollectionView Providers { get; }
        Task LoadAsync();
    }
}