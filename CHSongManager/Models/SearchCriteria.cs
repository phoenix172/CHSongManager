using System;
using System.ComponentModel;
using System.Threading;
using TinyMVVM;

namespace CHSongManager.Models
{
    public class SearchCriteria
    {
        public static readonly SearchCriteria Empty = new SearchCriteria();

        public string Artist { get; set; }

        public string Name { get; set; }

        public string Album { get; set; }
    }
}