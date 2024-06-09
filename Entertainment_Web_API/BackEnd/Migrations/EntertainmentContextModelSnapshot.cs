﻿// <auto-generated />
using System;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BackEnd.Migrations
{
    [DbContext(typeof(EntertainmentContext))]
    partial class EntertainmentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BackEnd.Models.Comment", b =>
                {
                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateOnly?>("CommentPostingTime")
                        .HasColumnType("date");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("CommentId");

                    b.HasIndex("Id");

                    b.HasIndex("VideoId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("BackEnd.Models.Playlist", b =>
                {
                    b.Property<string>("PlaylistId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PlaylistName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VideoCount")
                        .HasColumnType("int");

                    b.HasKey("PlaylistId");

                    b.HasIndex("Id");

                    b.ToTable("Playlist");
                });

            modelBuilder.Entity("BackEnd.Models.ReplyComment", b =>
                {
                    b.Property<string>("ReplyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CommentId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("DisLike")
                        .HasColumnType("int");

                    b.Property<int>("Like")
                        .HasColumnType("int");

                    b.Property<string>("ReplyContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly?>("ReplyPostingTime")
                        .HasColumnType("date");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ReplyId");

                    b.HasIndex("CommentId");

                    b.HasIndex("UserId");

                    b.ToTable("ReplyComments");
                });

            modelBuilder.Entity("BackEnd.Models.UserVideoReaction", b =>
                {
                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(0);

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.Property<bool>("IsDisliked")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.HasKey("VideoId", "Id")
                        .HasName("PK__UV__0AC8567A7A214FA3");

                    b.HasIndex("Id");

                    b.ToTable("UserVideoReaction", (string)null);
                });

            modelBuilder.Entity("BackEnd.Models.Video", b =>
                {
                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Dislikes")
                        .HasColumnType("int");

                    b.Property<int?>("Likes")
                        .HasColumnType("int");

                    b.Property<string>("ThumbnailUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VideoCategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VideoDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("VideoPostingTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VideoViews")
                        .HasColumnType("int");

                    b.HasKey("VideoId");

                    b.HasIndex("VideoCategoryId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("BackEnd.Models.VideoCategory", b =>
                {
                    b.Property<string>("VideoCategoryId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("VideoCategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VideoCategoryId");

                    b.ToTable("VideoCategories");
                });

            modelBuilder.Entity("BackEnd.Models.VideoPlaylist", b =>
                {
                    b.Property<string>("VideoId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(0);

                    b.Property<string>("PlaylistId")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnOrder(1);

                    b.HasKey("VideoId", "PlaylistId")
                        .HasName("PK__VideoPla__0AC8567A7A214FA2");

                    b.HasIndex("PlaylistId");

                    b.ToTable("VideoPlaylist", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BackEnd.Models.AppUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Avtprofile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNamee")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("AppUser");
                });

            modelBuilder.Entity("BackEnd.Models.Comment", b =>
                {
                    b.HasOne("BackEnd.Models.AppUser", "AppUser")
                        .WithMany("Comments")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BackEnd.Models.Video", "Video")
                        .WithMany("Comments")
                        .HasForeignKey("VideoId");

                    b.Navigation("AppUser");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("BackEnd.Models.Playlist", b =>
                {
                    b.HasOne("BackEnd.Models.AppUser", "AppUser")
                        .WithMany("Playlists")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AppUser");
                });

            modelBuilder.Entity("BackEnd.Models.ReplyComment", b =>
                {
                    b.HasOne("BackEnd.Models.Comment", "Comment")
                        .WithMany("Replies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BackEnd.Models.AppUser", "AppUser")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("AppUser");

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("BackEnd.Models.UserVideoReaction", b =>
                {
                    b.HasOne("BackEnd.Models.AppUser", "AppUser")
                        .WithMany("UserVideoReaction")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__UV__User__6B24EA83");

                    b.HasOne("BackEnd.Models.Video", "Video")
                        .WithMany("UserVideoReactions")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__UV__Video__6A30C640");

                    b.Navigation("AppUser");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("BackEnd.Models.Video", b =>
                {
                    b.HasOne("BackEnd.Models.VideoCategory", "VideoCategory")
                        .WithMany("Videos")
                        .HasForeignKey("VideoCategoryId");

                    b.Navigation("VideoCategory");
                });

            modelBuilder.Entity("BackEnd.Models.VideoPlaylist", b =>
                {
                    b.HasOne("BackEnd.Models.Playlist", "Playlist")
                        .WithMany("VideoPlaylists")
                        .HasForeignKey("PlaylistId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__VideoPlay__Playl__6B24EA82");

                    b.HasOne("BackEnd.Models.Video", "Video")
                        .WithMany("VideoPlaylists")
                        .HasForeignKey("VideoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__VideoPlay__Video__6A30C649");

                    b.Navigation("Playlist");

                    b.Navigation("Video");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BackEnd.Models.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("BackEnd.Models.Playlist", b =>
                {
                    b.Navigation("VideoPlaylists");
                });

            modelBuilder.Entity("BackEnd.Models.Video", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("UserVideoReactions");

                    b.Navigation("VideoPlaylists");
                });

            modelBuilder.Entity("BackEnd.Models.VideoCategory", b =>
                {
                    b.Navigation("Videos");
                });

            modelBuilder.Entity("BackEnd.Models.AppUser", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Playlists");

                    b.Navigation("UserVideoReaction");
                });
#pragma warning restore 612, 618
        }
    }
}
