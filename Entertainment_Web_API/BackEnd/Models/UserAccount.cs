using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class UserAccount
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Avatar { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Music> Musics { get; set; } = new List<Music>();

    public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();

    public virtual UserRole? Role { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
