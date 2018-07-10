using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class NullSongProvider : ISongProvider
    {
        public NullSongProvider(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Task<IEnumerable<ISong>> GetAsync(SearchCriteria criteria)
        {
            return Task.FromResult(Enumerable.Empty<ISong>());
        }
    }
}