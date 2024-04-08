using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Music
{
    public int MusicId { get; set; }

    public string? MusicName { get; set; }

    public int? MusicCategoryId { get; set; }

    public int? UserId { get; set; }

    public virtual MusicCategory? MusicCategory { get; set; }

    public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();

    public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; } = new List<MusicPlaylist>();

    public virtual UserAccount? User { get; set; }
}
