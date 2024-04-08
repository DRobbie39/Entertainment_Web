using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class MusicCategory
{
    public int MusicCategoryId { get; set; }

    public string? MusicCategoryName { get; set; }

    public virtual ICollection<Music> Musics { get; set; } = new List<Music>();
}
