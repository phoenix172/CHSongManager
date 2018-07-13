using System.Threading.Tasks;
using ChorusLib;
using CHSongManager.Models;

namespace CHSongManager.Services.Interfaces
{
    public interface ISongDownloader : IConfigurable
    {
        Task<bool> DownloadAsync(DownloadableSong song);
    }
}