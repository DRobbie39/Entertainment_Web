using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Playlist
{
    public int PlaylistId { get; set; }

    public string? PlaylistName { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<MusicPlaylist> MusicPlaylists { get; set; } = new List<MusicPlaylist>();

    public virtual UserAccount? User { get; set; }
}
