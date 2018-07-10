using CHSongManager.Services.Interfaces;
using System.Windows.Input;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface ISearchViewModel
    {
        string Name { get; set; }
        string Artist { get; set; }
        string Album { get; set; }
        ICommand RemoteSearchCommand { get; set; }
        ITimer Timer { set; }
    }
}