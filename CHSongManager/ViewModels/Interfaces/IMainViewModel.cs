using TinyMVVM.Interfaces;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface IMainViewModel
    {
        IConfigurationViewModel ConfigurationVM { get; }
        ISongsViewModel SongsVM { get; }
    }
}