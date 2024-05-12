using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using BackEnd.Models;
using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data;

public partial class EntertainmentContext : IdentityDbContext
{
    //Constructor
    public EntertainmentContext(DbContextOptions options) : base(options)
    {

    }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoCategory> VideoCategories { get; set; }

    public virtual DbSet<AppUser> AppUser { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Playlist> Playlist { get; set; }

    public virtual DbSet<VideoPlaylist> VideoPlaylists { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-UTPMHK27\\SQLEXPRESS;Initial Catalog=Entertainment;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<VideoPlaylist>(entity =>
        {
            entity.HasKey(e => new { e.VideoId, e.PlaylistId }).HasName("PK__VideoPla__0AC8567A7A214FA2");

            entity.ToTable("VideoPlaylist");

            entity.HasOne(d => d.Video).WithMany(p => p.VideoPlaylists)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VideoPlay__Video__6A30C649");

            entity.HasOne(d => d.Playlist).WithMany(p => p.VideoPlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VideoPlay__Playl__6B24EA82");
        });

        OnModelCreatingPartial(builder);
    }

    partial void OnModelCreatingPartial(ModelBuilder builder); 
}