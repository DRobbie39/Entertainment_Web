using System;
using System.Collections.Generic;

namespace BackEnd.Models;

public partial class MusicOwner
{
    public int MusicId { get; set; }

    public int SingerId { get; set; }

    public DateOnly? ReleaseDate { get; set; }

    public virtual Music Music { get; set; } = null!;

    public virtual Singer Singer { get; set; } = null!;
}
