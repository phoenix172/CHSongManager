using System.Threading.Tasks;
using ChorusLib;

namespace CHSongManager.Services.Interfaces
{
    public interface ISongDownloader : IConfigurable
    {
        Task DownloadAsync(Song song);
    }
}