using System.Windows;
using CHSongManager.Infrastructure.Interfaces;
using CHSongManager.ViewModels;
using CHSongManager.ViewModels.Interfaces;

namespace CHSongManager.Infrastructure
{
    public class WindowView : Window, IView
    {
        public WindowView()
        {

        }

        public IViewModel ViewModel
        {
            get => DataContext as IViewModel;
            set => DataContext = value;
        }
    }
}
