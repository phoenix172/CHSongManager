using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;

namespace CHSongManager.Helpers
{
    public class ProviderMapper
    {
        private readonly ISongDataSource _dataSource;
        private readonly ISearchViewModel _searchViewModel;

        public ProviderMapper()
        :this(null, null)
        {
            
        }

        public ProviderMapper(ISongDataSource dataSource, ISearchViewModel searchViewModel)
        {
            _dataSource = dataSource;
            _searchViewModel = searchViewModel;
        }

        public IProviderViewModel Map(ISongProvider provider)
        {
            switch (provider)
            {
                case ChorusSongProvider chorus: return new ChorusProviderViewModel(_dataSource, chorus, _searchViewModel);
                default: return new NullProviderViewModel(provider);
            }
        }
    }
}
