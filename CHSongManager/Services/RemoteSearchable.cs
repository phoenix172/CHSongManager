using System;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class RemoteSearchable : ISearchable
    {
        private readonly ISongDataSource _dataSource;
        private readonly ISearchable _searchable;

        public RemoteSearchable(ISongDataSource dataSource, ISearchable searchable)
        {
            _dataSource = dataSource;
            _searchable = searchable;
        }

        public async Task SearchAsync(SearchCriteria criteria)
        {
            await _searchable.SearchAsync(criteria);
            //await _dataSource.ReloadAsync();
        }
    }
}