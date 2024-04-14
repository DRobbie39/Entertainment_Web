using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class MusicCategory
    {
        [Key]
        public string? MusicCategoryId { get; set; }
        public string? MusicCategoryName { get; set; }

        public virtual ICollection<Music> Musics { get; set; } = new List<Music>();
    }
}
