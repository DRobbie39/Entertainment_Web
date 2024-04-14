using Google.Apis.YouTube.v3.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Video
    {
        [Key] //Set khóa chính bằng data annotations
        public string VideoId { get; set; } = null!;
        public string? Title { get; set; }
        public string? VideoDescription { get; set; }
        public int? VideoViews { get; set; }
        public int? Likes { get; set; }
        public int? Dislikes { get; set; }
        public DateTimeOffset? VideoPostingTime { get; set; }
        public string? VideoUrl { get; set; }
        public string? ThumbnailUrl { get; set; }

		[ForeignKey("VideoCategory")] //Set khóa ngoại
		public string? VideoCategoryId { get; set; }
		public virtual VideoCategory? VideoCategory { get; set; }

		[ForeignKey("AppUser")]
		public string? Id { get; set; }
        public virtual AppUser? AppUser { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
