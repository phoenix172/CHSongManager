using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Properties;
using CHSongManager.Services.Interfaces;
using CHSongManager.ViewModels;

namespace CHSongManager.Services
{
    public class ConfigurationOptions : IConfigurationOptions
    {
        private static Settings Settings => Settings.Default;
        public string SongFolder
        {
            get => Settings.SongFolder;
            set => Settings.SongFolder = value;
        }

        public bool HasValidConfiguration() 
            => Directory.Exists(SongFolder);

        public void Save()
        {
            Settings.Save();
        }
    }
}
