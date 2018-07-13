using CHSongManager.Services.Interfaces;

namespace CHSongManager.ViewModels
{
    public class NullProviderViewModel : IProviderViewModel
    {

        public NullProviderViewModel(ISongProvider provider)
        {
            SongProvider = provider;
        }
        public ISongProvider SongProvider { get; }
    }
}