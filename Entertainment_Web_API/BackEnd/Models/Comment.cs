using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Comment
{
    public string CommentId { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateOnly? CommentPostingTime { get; set; }

    public string? VideoId { get; set; }

    public int? UserId { get; set; }

    public virtual UserAccount? User { get; set; }

    public virtual Video? Video { get; set; }
}
