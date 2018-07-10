using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TinyMVVM.Interfaces;

namespace TinyMVVM.Extensions
{
    public static class WindowManagerExtensions
    {
        public static void ConfigureMaximized<TViewModel>(this IWindowManager windowManager)
        {
            windowManager.WindowCreated += (_, window) =>
            {
                if (window.DataContext is TViewModel)
                {
                    window.SizeToContent = SizeToContent.Manual;
                    window.WindowState = WindowState.Maximized;
                }
            };
        }
    }
}
