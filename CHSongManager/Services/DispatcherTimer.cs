using System;
using System.Windows.Threading;
using CHSongManager.Services.Interfaces;

namespace CHSongManager.Services
{
    public class DispatcherTimer : ITimer
    {
        private readonly System.Windows.Threading.DispatcherTimer _dispatcher;

        public DispatcherTimer()
        {
            _dispatcher = new System.Windows.Threading.DispatcherTimer();
            _dispatcher.Tick += DispatcherTick;
        }

        public double Interval
        {
            get => _dispatcher.Interval.TotalMilliseconds;
            set => _dispatcher.Interval = TimeSpan.FromMilliseconds(value);
        }

        public void Start()
        {
            _dispatcher.Start();
        }

        public event EventHandler Elapsed;

        private void DispatcherTick(object sender, EventArgs e)
        {
            _dispatcher.Stop();
            Elapsed?.Invoke(sender, e);
        }
    }
}