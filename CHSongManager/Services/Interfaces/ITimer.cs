using System;

namespace CHSongManager.Services.Interfaces
{
    public interface ITimer
    {
        double Interval { get; set; }
        void Start();
        event EventHandler Elapsed;
    }
}