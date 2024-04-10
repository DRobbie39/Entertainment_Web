namespace BackEnd.Models
{
    public class MusicPlaylist
    {
        public string? MusicId { get; set; }

        public string? PlaylistId { get; set; }

        public DateOnly? AddedDate { get; set; }

        public virtual Music Music { get; set; } = null!;

        public virtual Playlist Playlist { get; set; } = null!;
    }
}
