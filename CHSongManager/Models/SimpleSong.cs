using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHSongManager.Models.Interfaces;

namespace CHSongManager.Models
{
    class SimpleSong : ISong
    {
        public SimpleSong()
        {
            
        }

        public SimpleSong(string name, string artist, string album)
        {
            Artist = artist;
            Name = name;
            Album = album;
        }

        public string Artist { get; set; }
        public string Name { get; set; }
        public string Album { get; set; }
    }
}
