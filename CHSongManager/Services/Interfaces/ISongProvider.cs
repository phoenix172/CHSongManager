using System.Collections.Generic;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;

namespace CHSongManager.Services.Interfaces
{
    public interface ISongProvider
    {
        string Name { get; }
        Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria);
    }
}