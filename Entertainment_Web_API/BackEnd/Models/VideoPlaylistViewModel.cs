namespace BackEnd.Models
{
    public class VideoPlaylistViewModel
    {
        public Video Video { get; set; }
        public List<Playlist> Playlists { get; set; }

        public string UserId { get; set; } // Thêm thuộc tính UserId
        public string VideoId { get; set; } // Thêm thuộc tính VideoId
    }
}
