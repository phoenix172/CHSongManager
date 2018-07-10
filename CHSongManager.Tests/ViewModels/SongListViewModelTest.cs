using System.ComponentModel;
using CHSongManager.Services.Interfaces;
using CHSongManager.Tests.Helpers;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;
using NSubstitute;
using NUnit.Framework;
using TinyMVVM;

namespace CHSongManager.Tests.ViewModels
{
    [TestFixture]
    public class SongListViewModelTest
    {
        private ISongListViewModel _viewModel;
        private ISongDataSource _dataSource;
        private IDialogService _dialogService;

        [SetUp]
        public void SetUp()
        {
            _dialogService = new DialogService();
            _dataSource = Substitute.For<ISongDataSource>();
            _viewModel = new SongListViewModel(_dataSource, _dialogService);
        }

        [Test]
        public void PropertyChanged_Propagates_FromDataSourceSongs_ToViewModelSongs()
        {
            PropertyChangedAssert.NotificationPropagates(_dataSource, nameof(ISongDataSource.Songs),
                _viewModel, nameof(ISongListViewModel.Songs));
        }

        [Test]
        public void PropertyChanged_Propagates_FromDataSourceIsLoading_ToViewModelIsLoading()
        {
            PropertyChangedAssert.NotificationPropagates(_dataSource, nameof(ISongDataSource.IsLoading),
                _viewModel, nameof(ISongListViewModel.IsLoading));
        }
                
    }
}