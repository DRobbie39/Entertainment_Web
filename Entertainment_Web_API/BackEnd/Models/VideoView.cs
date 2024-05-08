namespace BackEnd.Models
{
    public class VideoView
    {
        public Video CurrentVideo { get; set; }
        public List<Video> RelatedVideos { get; set; } = new List<Video>();
    }
}
