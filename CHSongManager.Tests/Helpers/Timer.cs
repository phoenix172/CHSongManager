using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Tests.Helpers
{
    public class Timer : ITimer
    {
        private System.Threading.Timer _timer;
        public Timer()
        {
        }

        public double Interval { get; set; }

        public void Start()
        {
            _timer = new System.Threading.Timer(TimerElapsed, null, TimeSpan.Zero,
                TimeSpan.FromMilliseconds(Interval));
        }

        public event EventHandler Elapsed;

        private void TimerElapsed(object state)
        {
            Elapsed?.Invoke(this, EventArgs.Empty);
            _timer.Dispose();
        }
    }
}
