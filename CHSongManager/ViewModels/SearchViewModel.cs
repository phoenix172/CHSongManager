using System;
using System.Linq;
using CHSongManager.Models;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using System.Runtime.CompilerServices;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using TinyMVVM;
using TinyMVVM.Utilities;

namespace CHSongManager.ViewModels
{
    public class SearchViewModel : ViewModelBase, ISearchViewModel
    {
        public const int SearchWaitTimeMilliseconds = 2000; 
        private readonly ISongDataSource _songDataSource;
        private ITimer _searchTimer;
        private static readonly string[] SearchProperties = {
            nameof(Name), nameof(Artist), nameof(Album)
        };

        public SearchViewModel()
            : this(new MockSongDataSource())
        {
            
        }

        public ITimer Timer
        {
            set => InitSearchTimer(value);
        }

        public SearchViewModel(ISongDataSource songDataSource)
        {
            Guard.NotNull(songDataSource, nameof(songDataSource));
            _songDataSource = songDataSource;
            PropagateChanges(_songDataSource, nameof(ISongDataSource.IsRemoteSearch));

            RemoteSearchCommand = new RelayCommand(SearchAsync);
            InitSearchTimer();
            PropertyChanged += SearchViewModel_PropertyChanged;
        }

        private SearchCriteria Criteria => _songDataSource.Criteria;
        public string Artist { get => Criteria.Artist; set => Criteria.Artist = value; }
        public string Name { get => Criteria.Name; set => Criteria.Name = value; }
        public string Album { get => Criteria.Album; set => Criteria.Album = value; }

        public bool IsRemoteSearch => _songDataSource.IsRemoteSearch;

        public ICommand RemoteSearchCommand { get; set; }

        private void InitSearchTimer(ITimer timer = null)
        {
            _searchTimer = timer ?? new DispatcherTimer();
            _searchTimer.Interval = SearchWaitTimeMilliseconds;
            _searchTimer.Elapsed += SearchTimerElapsed;
        }

        private void SearchTimerElapsed(object sender, EventArgs e)
        {
            SearchAsync();
        }

        private void SearchViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (IsSearchProperty(e.PropertyName))
            {
                _searchTimer.Start();
            }
        }

        private bool IsSearchProperty(string propertyName)
        {
            return SearchProperties.Contains(propertyName);
        }

        private async void SearchAsync()
        {
            OnSearching();
            await _songDataSource.LoadAsync();
        }

        public event EventHandler Searching;

        protected virtual void OnSearching()
        {
            Searching?.Invoke(this, EventArgs.Empty);
        }
    }
}