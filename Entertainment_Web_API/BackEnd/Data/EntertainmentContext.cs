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

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<MusicCategory> MusicCategories { get; set; }

    public virtual DbSet<MusicOwner> MusicOwners { get; set; }

    public virtual DbSet<MusicPlaylist> MusicPlaylists { get; set; }

    public virtual DbSet<Singer> Singers { get; set; }



    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-UTPMHK27\\SQLEXPRESS;Initial Catalog=Entertainment;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<MusicPlaylist>(entity =>
        {
            entity.HasKey(e => new { e.MusicId, e.PlaylistId }).HasName("PK__MusicPla__0AC8567A7A214FA2");

            entity.ToTable("MusicPlaylist");

            entity.HasOne(d => d.Music).WithMany(p => p.MusicPlaylists)
                .HasForeignKey(d => d.MusicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MusicPlay__Music__6A30C649");

            entity.HasOne(d => d.Playlist).WithMany(p => p.MusicPlaylists)
                .HasForeignKey(d => d.PlaylistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MusicPlay__Playl__6B24EA82");
        });

        builder.Entity<MusicOwner>(entity =>
        {
            entity.HasKey(e => new { e.MusicId, e.SingerId }).HasName("PK__MusicOwn__A11E3E7ACF167879");

            entity.ToTable("MusicOwner");

            entity.HasOne(d => d.Music).WithMany(p => p.MusicOwners)
                .HasForeignKey(d => d.MusicId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MusicOwne__Music__628FA481");

            entity.HasOne(d => d.Singer).WithMany(p => p.MusicOwners)
                .HasForeignKey(d => d.SingerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MusicOwne__Singe__6383C8BA");
        });

        OnModelCreatingPartial(builder);
    }

    partial void OnModelCreatingPartial(ModelBuilder builder); 
}