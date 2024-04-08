using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class VideoCategory
{
    public string VideoCategoryId { get; set; } = null!;

    public string? VideoCategoryName { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
