using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace CHSongManager.Tests.ViewModels
{
    [TestFixture]
    public class SelectProviderViewModelTest
    {
        private ISongDataSource _dataSource;
        private ISelectProviderViewModel _viewModel;
        private IEnumerable<ISongProvider> _providers;

        [SetUp]
        public void SetUp()
        {
            _dataSource = Substitute.For<ISongDataSource>();
            _providers = SongProvider.MockProviders();
            var providerManager = new ProviderManager(_providers);
            _dataSource.Providers.Returns(providerManager);
            _dataSource.CurrentProvider.Returns(c=>providerManager.Current);
            _viewModel = new SelectProviderViewModel(_dataSource);
        }

        [Test]
        public async Task Load_Initializes_ProvidersCollection()
        {
            await _viewModel.LoadAsync();

            Assert.That(_viewModel.Providers, Is.EquivalentTo(_providers));
        }

        [Test]
        public async Task Load_SetsCurrentProvider_ToFirst()
        {
            await _viewModel.LoadAsync();

            Assert.That(_viewModel.Providers.CurrentItem, Is.EqualTo(_providers.First()));
        }

        [Test]
        public async Task Load_SetsCurrentDataSourceProvider_ToFirst()
        {
            await _viewModel.LoadAsync();

            Assert.That(_dataSource.CurrentProvider, Is.EqualTo(_providers.First()));
        }

        [Test]
        public async Task Providers_ChangeCurrent_SetsCurrentDataSourceProvider()
        {
            await _viewModel.LoadAsync();
            _viewModel.Providers.MoveCurrentToLast();

            Assert.That(_dataSource.CurrentProvider, Is.EqualTo(_providers.Last()));
        }

        [Test]
        public async Task Providers_ChangeCurrent_CallsDataSourceReloadAsync()
        {
            await _viewModel.LoadAsync();
            _viewModel.Providers.MoveCurrentToLast();

            await _dataSource.Received().LoadAsync();
        }
    }
}
