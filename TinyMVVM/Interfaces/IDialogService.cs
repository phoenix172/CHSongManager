namespace TinyMVVM
{
    public interface IDialogService
    {
        void ShowError(string error);
        bool ShowFolderBrowser(string currentPath, out string selectedPath);
    }
}
