namespace CHSongManager.Services.Interfaces
{
    public interface IConfigurationOptions
    {
        string SongFolder { get; set; }
        bool HasValidConfiguration();
        void Save();
    }
}