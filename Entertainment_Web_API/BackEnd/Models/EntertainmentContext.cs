using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models;

public partial class EntertainmentContext : DbContext
{
    public EntertainmentContext()
    {
    }

    public EntertainmentContext(DbContextOptions<EntertainmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Music> Musics { get; set; }

    public virtual DbSet<MusicCategory> MusicCategories { get; set; }

    public virtual DbSet<MusicOwner> MusicOwners { get; set; }

    public virtual DbSet<MusicPlaylist> MusicPlaylists { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Singer> Singers { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    public virtual DbSet<VideoCategory> VideoCategories { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=LAPTOP-UTPMHK27\\SQLEXPRESS;Initial Catalog=Entertainment;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCA23F3889E");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId).HasMaxLength(255);
            entity.Property(e => e.VideoId).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Comment__UserId__571DF1D5");

            entity.HasOne(d => d.Video).WithMany(p => p.Comments)
                .HasForeignKey(d => d.VideoId)
                .HasConstraintName("FK__Comment__VideoId__5629CD9C");
        });

        modelBuilder.Entity<Music>(entity =>
        {
            entity.HasKey(e => e.MusicId).HasName("PK__Music__11F84000291EA5D1");

            entity.ToTable("Music");

            entity.Property(e => e.MusicName).HasMaxLength(500);

            entity.HasOne(d => d.MusicCategory).WithMany(p => p.Musics)
                .HasForeignKey(d => d.MusicCategoryId)
                .HasConstraintName("FK__Music__MusicCate__5BE2A6F2");

            entity.HasOne(d => d.User).WithMany(p => p.Musics)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Music__UserId__5CD6CB2B");
        });

        modelBuilder.Entity<MusicCategory>(entity =>
        {
            entity.HasKey(e => e.MusicCategoryId).HasName("PK__MusicCat__740DDDFB4E7B2706");

            entity.ToTable("MusicCategory");

            entity.Property(e => e.MusicCategoryName).HasMaxLength(20);
        });

        modelBuilder.Entity<MusicOwner>(entity =>
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

        modelBuilder.Entity<MusicPlaylist>(entity =>
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

        modelBuilder.Entity<Playlist>(entity =>
        {
            entity.HasKey(e => e.PlaylistId).HasName("PK__Playlist__B30167A0E138ADEF");

            entity.ToTable("Playlist");

            entity.Property(e => e.PlaylistName).HasMaxLength(500);

            entity.HasOne(d => d.User).WithMany(p => p.Playlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Playlist__UserId__66603565");
        });

        modelBuilder.Entity<Singer>(entity =>
        {
            entity.HasKey(e => e.SingerId).HasName("PK__Singer__0E67E7AA7009344B");

            entity.ToTable("Singer");

            entity.Property(e => e.SingerName).HasMaxLength(500);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserAcco__1788CC4C01E5571D");

            entity.ToTable("UserAccount");

            entity.Property(e => e.Avatar).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserPassword).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.UserAccounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserAccou__RoleI__4BAC3F29");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__UserRole__8AFACE1A7F899F8E");

            entity.ToTable("UserRole");

            entity.Property(e => e.RoleName).HasMaxLength(20);
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("PK__Video__BAE5126AF3758DAE");

            entity.ToTable("Video");

            entity.Property(e => e.VideoId).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(500);
            entity.Property(e => e.VideoCategoryId).HasMaxLength(255);

            entity.HasOne(d => d.User).WithMany(p => p.Videos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Video__UserId__52593CB8");

            entity.HasOne(d => d.VideoCategory).WithMany(p => p.Videos)
                .HasForeignKey(d => d.VideoCategoryId)
                .HasConstraintName("FK__Video__VideoCate__5165187F");
        });

        modelBuilder.Entity<VideoCategory>(entity =>
        {
            entity.HasKey(e => e.VideoCategoryId).HasName("PK__VideoCat__A41319A34CFBC46F");

            entity.ToTable("VideoCategory");

            entity.Property(e => e.VideoCategoryId).HasMaxLength(255);
            entity.Property(e => e.VideoCategoryName).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
