namespace CHSongManager.Services.Interfaces
{
    public interface IConfigurationService
    {
        IConfigurationOptions Options { get; }
        bool Configure(bool force = false);
    }
}