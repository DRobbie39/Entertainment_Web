using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Video
{
    public string VideoId { get; set; } = null!;

    public string? Title { get; set; }

    public string? VideoDescription { get; set; }

    public int? VideoViews { get; set; }

    public int? Likes { get; set; }

    public int? Dislikes { get; set; }

    public DateOnly? VideoPostingTime { get; set; }

    public string? VideoUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? VideoCategoryId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual UserAccount? User { get; set; }

    public virtual VideoCategory? VideoCategory { get; set; }
}
