using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CHSongManager.Services.Interfaces;
using TinyMVVM.Utilities;

namespace CHSongManager.Services
{
    public class ProviderManager : IConfigurable, IReadOnlyCollection<ISongProvider>
    {
        private ISongProvider _current;
        private readonly IReadOnlyCollection<ISongProvider> _providers;

        public ProviderManager(IEnumerable<ISongProvider> providers)
        {
            _providers = providers.ToList().AsReadOnly();
            Current = _providers.First();
        }

        public ISongProvider Current
        {
            get => _current;
            set
            {
                if (_current == value)
                    return;

                Guard.NotNull(value, nameof(Current));
                if(!_providers.Contains(value))
                    throw new ArgumentOutOfRangeException(nameof(Current),
                        value,"The supplied provider is not owned by this ProviderManager");
                _current = value;
                OnCurrentProviderChanged();
            }
        }

        public event EventHandler CurrentProviderChanged;

        protected virtual void OnCurrentProviderChanged()
        {
            CurrentProviderChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ApplyConfiguration(IConfigurationOptions options)
        {
            foreach (var songProvider in _providers)
            {
                if(songProvider is IConfigurable configurable)
                    configurable.ApplyConfiguration(options);
            }
        }

        public IEnumerator<ISongProvider> GetEnumerator()
        {
            return _providers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _providers).GetEnumerator();
        }

        public int Count => _providers.Count;
    }
}