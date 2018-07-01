using System;
using System.ComponentModel;
using System.Windows.Input;
using CHSongManager.Infrastructure;
using CHSongManager.Infrastructure.Interfaces;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface IConfigurationViewModel : INotifyPropertyChanged, IViewModel
    {
        ICommand SelectFolderCommand { get; }
        string SongFolder { get; set; }
        ICommand OKCommand { get; }
    }
}