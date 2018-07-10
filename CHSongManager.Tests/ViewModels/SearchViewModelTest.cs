//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Threading;
//using System.Threading.Tasks;
//using CHSongManager.Models;
//using CHSongManager.Services.Interfaces;
//using CHSongManager.Tests.Helpers;
//using CHSongManager.ViewModels;
//using CHSongManager.ViewModels.Interfaces;
//using NSubstitute;
//using NUnit.Framework;

//namespace CHSongManager.Tests.ViewModels
//{
//    [TestFixture]
//    public class SearchViewModelTest
//    {
//        private ISearchViewModel _viewModel;
//        private ISongDataSource _dataSource;

//        [SetUp]
//        public void SetUp()
//        {
//            _dataSource = Substitute.For<ISongDataSource>();
//            _dataSource.SearchCriteria.Returns(new SearchCriteria());
//            _viewModel = new SearchViewModel(_dataSource);
//        }

//        private static Stopwatch stopwatch;
//        private static int counter = 0;
//        [TestCaseSource(nameof(SongPropertyTestCases))]
//        public void SongProperty_Changed_UpdatesDataSourceSearchCriteria_AfterSearchWaitMilliseconds(
//            (Func<ISongDataSource, string> getter, Action<ISearchViewModel, string> setter) testCase)
//        {
//            counter++;
//            if (counter == 2) Debugger.Break();
//            stopwatch = Stopwatch.StartNew();

//            string newValue = Guid.NewGuid().ToString();
//            _viewModel.Timer = new Helpers.Timer();

//            testCase.setter(_viewModel, newValue);

//            Assert.That(testCase.getter(_dataSource) == newValue, Is.Not.True);
//            Assert.That(() => testCase.getter(_dataSource) == newValue,
//                Is.True.After(SearchViewModel.SearchWaitTimeMilliseconds).MilliSeconds);
//        }

//        [Test]
//        public void NameProperty_Changed_UpdatesDataSourceSearchCriteria_AfterSearchWaitMilliseconds()
//        {
//            int searchWait = SearchViewModel.SearchWaitTimeMilliseconds;
//            string newValue = Guid.NewGuid().ToString();
//            _viewModel.Timer = new Helpers.Timer();

//            _viewModel.Name = newValue;

//            Assert.That(_dataSource.SearchCriteria.Name == newValue, Is.Not.True);
//            Assert.That(_dataSource.SearchCriteria.Name == newValue,
//                Is.True.After(searchWait).MilliSeconds);
//        }

//        private static IEnumerable<
//            (Func<ISongDataSource, string> getter,
//            Action<ISearchViewModel, string> setter)> SongPropertyTestCases()
//        {
//            yield return (
//                Getter(ds => GetAndDebug(ds.SearchCriteria.Name, $"getting datasource->name {DateTime.Now}")),
//                Setter((vm, val) => vm.Name = GetAndDebug(val, $"setting searchVM->name {DateTime.Now}"))
//           );
//            yield return (
//                Getter(ds => ds.SearchCriteria.Artist), Setter((vm, val) => vm.Artist = val));
//            yield return (Getter(ds => ds.SearchCriteria.Album), Setter((vm, val) => vm.Album = val));
//        }

//        private static T GetAndDebug<T>(T value, string message)
//        {
//            Console.WriteLine(message);
//            return value;
//        }

//        private static Func<ISongDataSource, string> Getter(Func<ISongDataSource, string> getter)
//        {
//            return dataSource =>
//            {
//                Console.WriteLine($"Getter called at {stopwatch.ElapsedMilliseconds}");
//                return getter(dataSource);
//            };
//        }

//        private static Action<ISearchViewModel, string> Setter(Action<ISearchViewModel, string> setter)
//        {
//            return (viewModel, value) =>
//            {
//                Console.WriteLine($"Setter called at {stopwatch.ElapsedMilliseconds}");
//                setter(viewModel, value);
//            };
//        }
//    }
//}