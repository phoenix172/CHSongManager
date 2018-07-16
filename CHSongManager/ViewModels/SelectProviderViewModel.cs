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

        public SelectProviderViewModel()
            : this(new MockSongDataSource())
        {
            ThrowIfNotInDesignMode();
            LoadAsync();
        }

        public SelectProviderViewModel(ISongDataSource songDataSource)
        {
            _songDataSource = songDataSource;
        }

        public ICollectionView Providers { get; set; }

        public async Task LoadAsync()
        {
            Providers = CollectionViewSource.GetDefaultView(_songDataSource.Providers);
            Providers.CurrentChanged += async (s,e)=>await CurrentProviderChangedAsync();
            await CurrentProviderChangedAsync();
        }

        private async Task CurrentProviderChangedAsync()
        {
            _songDataSource.Providers.Current = Providers.CurrentItem as ISongProvider;
            await _songDataSource.LoadAsync();
        }
    }

    public interface ISelectProviderViewModel
    {
        ICollectionView Providers { get; }
        Task LoadAsync();
    }
}