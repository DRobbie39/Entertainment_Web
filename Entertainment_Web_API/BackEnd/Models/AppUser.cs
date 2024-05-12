﻿using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
        public string? UserNamee { get; set; }
        public string? Avtprofile { get; set; }

        public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Playlist> Playlists { get; set; } = new List<Playlist>();
    }
}
