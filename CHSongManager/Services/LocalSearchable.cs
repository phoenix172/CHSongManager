using System;
using System.ComponentModel;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class LocalSearchable : ISearchable
    {
        private readonly ICollectionView _collectionView;
        private SearchCriteria _searchCriteria;

        public LocalSearchable(ICollectionView collectionView)
        {
            _collectionView = collectionView;
        }

        public Task SearchAsync(SearchCriteria criteria)
        {
            _searchCriteria = criteria;
            return Task.Run(() =>
            {
                _collectionView.Filter = SearchPredicate;
                _collectionView.Refresh();
                _collectionView.Filter = null;
            }).ContinueWith(t=>throw t.Exception, TaskContinuationOptions.OnlyOnFaulted);
        }

        private bool SearchPredicate(object item)
        {
            if (!(item is ISong song)) return false;
            return (song.Name?.Contains(_searchCriteria.Name ?? "") ?? false) &&
                   (song.Album?.Contains(_searchCriteria.Album ?? "") ?? false) &&
                   (song.Artist?.Contains(_searchCriteria.Artist ?? "") ?? false);
        }
    }
}