using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class MusicOwner
    {
		[Key, Column(Order = 0)]
		[ForeignKey("Music")]
		public string? MusicId { get; set; }
		public virtual Music Music { get; set; } = null!;

		[Key, Column(Order = 1)]
		[ForeignKey("Singer")]
		public string? SingerId { get; set; }
		public virtual Singer Singer { get; set; } = null!;
		public DateOnly? ReleaseDate { get; set; }
    }
}