using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Comment
    {
		[Key]
		public string CommentId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateOnly? CommentPostingTime { get; set; }

		[ForeignKey("Video")]
		public string? VideoId { get; set; }
		public virtual Video? Video { get; set; }

		[ForeignKey("AppUser")]
		public string? Id { get; set; }
        public virtual AppUser? AppUser { get; set; }
    }
}
