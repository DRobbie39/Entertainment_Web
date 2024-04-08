using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class Singer
{
    public int SingerId { get; set; }

    public string? SingerName { get; set; }

    public virtual ICollection<MusicOwner> MusicOwners { get; set; } = new List<MusicOwner>();
}
