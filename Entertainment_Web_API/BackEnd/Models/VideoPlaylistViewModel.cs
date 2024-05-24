namespace BackEnd.Models
{
    public class VideoPlaylistViewModel
    {
        public Video Video { get; set; }
        public List<Playlist> Playlists { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
