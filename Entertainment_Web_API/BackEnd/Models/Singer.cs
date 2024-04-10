namespace BackEnd.Models
{
    public class Singer
    {
        public string? SingerId { get; set; }

        public string? SingerName { get; set; }

        public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();
    }
}
