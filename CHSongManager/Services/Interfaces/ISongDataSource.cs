using System.ComponentModel;
using System.Threading.Tasks;
using CHSongManager.Models;

namespace CHSongManager.Services.Interfaces
{
    public interface ISongDataSource : INotifyPropertyChanged, IConfigurable
    {
        ICollectionView Songs { get; }
        ISongProvider SongProvider { get; set; }
        bool IsRemoteSearch { get; }
        bool IsLoading { get; }
        SearchCriteria Criteria { get; }
        Task LoadAsync();
    }
}