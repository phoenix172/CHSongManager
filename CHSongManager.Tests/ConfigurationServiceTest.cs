using System;
using CHSongManager.Infrastructure;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace CHSongManager.Tests
{
    [TestFixture]
    public class ConfigurationServiceTest
    {
        private IWindowManager _windowManager;

        [SetUp]
        public void SetUp()
        {
            _windowManager = Substitute.For<IWindowManager>();
        }

        [Test]
        public void Configure_DoesNotHaveValidConfiguration_ShowsConfigurationWindow()
        {
            var configService = MakeConfigurationService(false);

            configService.Configure();

            _windowManager.Received().ShowDialog(Arg.Any<IConfigurationViewModel>());
        }

        [Test]
        public void Configure_HasValidConfiguration_DoesNotCallWindowManager()
        {
            var configService = MakeConfigurationService(true);

            configService.Configure();

            _windowManager.DidNotReceive().ShowDialog(Arg.Any<IViewModel>());
        }

        [Test]
        public void Configure_HasValidConfigurationAndForce_ShowsConfigurationWindow()
        {
            var configService = MakeConfigurationService(true);

            configService.Configure(true);

            _windowManager.Received().ShowDialog(Arg.Any<IConfigurationViewModel>());
        }

        private ConfigurationService MakeConfigurationService(bool hasValidConfiguration)
        {
            ConfigurationService configService = new ConfigurationService(_windowManager);
            configService.Options = Substitute.For<IConfigurationOptions>();
            configService.Options.HasValidConfiguration().Returns(hasValidConfiguration);
            return configService;
        }
    }
}
