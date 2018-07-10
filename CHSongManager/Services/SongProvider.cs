using System.Collections.Generic;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public static class SongProvider
    {
        public static ISongProvider Null(string name)
        {
            return new NullSongProvider(name);
        }

        public static IEnumerable<ISongProvider> MockProviders()
        {
            return new[]
            {
                Null("Provider1"),
                Null("Provider2"),
                Null("Provider3"),
            };
        }
    }
}