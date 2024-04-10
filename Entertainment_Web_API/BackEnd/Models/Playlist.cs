namespace BackEnd.Models
{
    public class Playlist
    {
        public string? PlaylistId { get; set; }
        public string? PlaylistName { get; set; }
        public string? Id { get; set; }

        public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; } = new List<MusicPlaylist>();
        public virtual AppUser? AppUser { get; set; }
    }
}
