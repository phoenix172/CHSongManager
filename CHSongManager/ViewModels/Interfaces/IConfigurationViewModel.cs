using System;
using System.ComponentModel;
using System.Windows.Input;
using CHSongManager.Services.Interfaces;
using TinyMVVM.Interfaces;

namespace CHSongManager.ViewModels.Interfaces
{
    public interface IConfigurationViewModel : ICanClose
    {
        ICommand SelectFolderCommand { get; }
        string SongFolder { get; set; }
        ICommand OKCommand { get; }
        IConfigurationOptions Options { get; }
    }
}