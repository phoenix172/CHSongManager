using CHSongManager.Services.Interfaces;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface ISongsViewModel
    {
        ISearchViewModel SearchVM { get; }
        ISongListViewModel SongListVM { get; }
    }
}