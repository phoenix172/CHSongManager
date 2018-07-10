namespace CHSongManager.Models.Interfaces
{
    public interface ISong
    {
        string Artist { get; }
        string Name { get; }
        string Album { get; }
    }
}