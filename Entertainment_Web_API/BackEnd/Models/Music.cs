using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Music
    {
        [Key]
        public string? MusicId { get; set; }
        public string? MusicName { get; set; }

		[ForeignKey("MusicCategory")]
		public string? MusicCategoryId { get; set; }
		public virtual MusicCategory? MusicCategory { get; set; }

		[ForeignKey("AppUser")]
		public string? Id { get; set; }
		public virtual AppUser? AppUser { get; set; }

		public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; } = new List<MusicPlaylist>();
		public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();
    }
}
