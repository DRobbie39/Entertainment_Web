namespace BackEnd.Models
{
    public class VideoCategory
    {
        public string VideoCategoryId { get; set; } = null!;
        public string? VideoCategoryName { get; set; }

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
    }
}
