using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CHSongManager.Models;
using CHSongManager.Models.Interfaces;

namespace CHSongManager.Services.Interfaces
{
    public interface ISearchable
    {
        Task SearchAsync(SearchCriteria criteria);
    }
}