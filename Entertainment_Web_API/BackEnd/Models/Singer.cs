using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Singer
    {
        [Key]
        public string? SingerId { get; set; }
        public string? SingerName { get; set; }

        public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();
    }
}
