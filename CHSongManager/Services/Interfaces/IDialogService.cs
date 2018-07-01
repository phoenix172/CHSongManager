using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CHSongManager.Services.Interfaces
{
    public interface IDialogService
    {
        void ShowError(string error);
        bool ShowFolderBrowser(string currentPath, out string selectedPath);
    }
}
