using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class MusicPlaylist
{
    public int MusicId { get; set; }

    public int PlaylistId { get; set; }

    public DateOnly? AddedDate { get; set; }

    public virtual Music Music { get; set; } = null!;

    public virtual Playlist Playlist { get; set; } = null!;
}
