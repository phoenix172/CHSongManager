using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;
using CHSongManager.Services;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels.Interfaces;
using Ninject;
using TinyMVVM;
using TinyMVVM.Utilities;

namespace CHSongManager.ViewModels
{
    public class SongListViewModel : ViewModelBase, ISongListViewModel
    {
        private readonly ISongDataSource _songDataSource;
        private readonly IDialogService _dialogService;

        public SongListViewModel()
            : this(new MockSongDataSource(), null)
        {
            ThrowIfNotInDesignMode();
        }

        [Inject]
        public SongListViewModel(ISongDataSource songDataSource, IDialogService dialogService)
        {
            Guard.NotNull(songDataSource, nameof(songDataSource));

            _dialogService = dialogService;
            _songDataSource = songDataSource;

            PropagateChanges(_songDataSource, nameof(ISongDataSource.Songs));
            PropagateChanges(_songDataSource, nameof(ISongDataSource.IsLoading));
        }

        public ICollectionView Songs => _songDataSource.Songs;
        public bool IsLoading => _songDataSource.IsLoading;
    }
}