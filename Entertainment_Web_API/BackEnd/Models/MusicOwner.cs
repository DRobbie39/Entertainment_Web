namespace BackEnd.Models
{
    public class MusicOwner
    {
        public string? MusicId { get; set; }

        public string? SingerId { get; set; }

        public DateOnly? ReleaseDate { get; set; }

        public virtual Music Music { get; set; } = null!;

        public virtual Singer Singer { get; set; } = null!;
    }
}