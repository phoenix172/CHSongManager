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
        private List<ISongProvider> _providers;
        private ISelectProviderViewModel _viewModel;

        [SetUp]
        public void SetUp()
        {
            _dataSource = Substitute.For<ISongDataSource>();
            _providers = SongProvider.MockProviders().ToList();
            _viewModel = new SelectProviderViewModel(_dataSource, _providers);
        }

        [Test]
        public void Load_Initializes_ProvidersCollection()
        {
            _viewModel.Load();

            Assert.That(_viewModel.Providers, Is.EquivalentTo(_providers));
        }

        [Test]
        public void Load_SetsCurrentProvider_ToFirst()
        {
            _viewModel.Load();

            Assert.That(_viewModel.Providers.CurrentItem, Is.EqualTo(_providers.First()));
        }

        [Test]
        public void Load_SetsCurrentDataSourceProvider_ToFirst()
        {
            _viewModel.Load();

            Assert.That(_dataSource.SongProvider, Is.EqualTo(_providers.First()));
        }

        [Test]
        public void Providers_ChangeCurrent_SetsCurrentDataSourceProvider()
        {
            _viewModel.Load();
            _viewModel.Providers.MoveCurrentToLast();

            Assert.That(_dataSource.SongProvider, Is.EqualTo(_providers.Last()));
        }

        [Test]
        public void Providers_ChangeCurrent_CallsDataSourceReloadAsync()
        {
            _viewModel.Load();
            _viewModel.Providers.MoveCurrentToLast();

            _dataSource.Received().LoadAsync();
        }
    }
}
