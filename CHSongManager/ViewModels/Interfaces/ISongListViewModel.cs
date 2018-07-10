using System.ComponentModel;
using CHSongManager.Models;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using TinyMVVM.Interfaces;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface ISongListViewModel : INotifyPropertyChanged
    {
        ICollectionView Songs { get; }
        bool IsLoading { get; }
    }
}