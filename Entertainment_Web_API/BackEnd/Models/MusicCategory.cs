namespace BackEnd.Models
{
    public class MusicCategory
    {
        public string? MusicCategoryId { get; set; }

        public string? MusicCategoryName { get; set; }

        public virtual ICollection<Music> Musics { get; set; } = new List<Music>();
    }
}
