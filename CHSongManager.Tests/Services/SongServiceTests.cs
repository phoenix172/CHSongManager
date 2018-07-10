using System.Linq;
using System.Threading.Tasks;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace CHSongManager.Tests.Services
{
    [TestFixture]
    public class SongServiceTests
    {
        [Test]
        public async Task Scan_LoadsTestSongs()
        {
            IConfigurationOptions configurationOptions = Substitute.For<IConfigurationOptions>();
            configurationOptions.SongFolder.Returns(TestContext.CurrentContext.TestDirectory + "\\Songs");
            var service = new LocalSongProvider(configurationOptions);
            var songs = await service.GetAsync();
            Assert.That(songs.ToList(), Has.Count.EqualTo(3));
        }
    }
}
