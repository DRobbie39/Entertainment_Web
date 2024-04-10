namespace BackEnd.Models
{
    public class Music
    {
        public string? MusicId { get; set; }

        public string? MusicName { get; set; }

        public string? MusicCategoryId { get; set; }

        public string? Id { get; set; }

        public virtual MusicCategory? MusicCategory { get; set; }

        public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();

        public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; } = new List<MusicPlaylist>();

        public virtual AppUser? AppUser { get; set; }
    }
}
