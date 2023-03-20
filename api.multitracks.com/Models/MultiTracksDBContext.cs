using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace api.multitracks.com.Models
{
    public partial class MultiTracksDBContext : DbContext
    {
        public MultiTracksDBContext()
        {
        }

        public MultiTracksDBContext(DbContextOptions<MultiTracksDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; } = null!;
        public virtual DbSet<Song> Songs { get; set; } = null!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.Biography)
                    .IsUnicode(false)
                    .HasColumnName("biography");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateCreation")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.HeroUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("heroURL");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("imageURL");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.ToTable("Song");

                entity.Property(e => e.SongId).HasColumnName("songID");

                entity.Property(e => e.AlbumId).HasColumnName("albumID");

                entity.Property(e => e.ArtistId).HasColumnName("artistID");

                entity.Property(e => e.Bpm)
                    .HasColumnType("decimal(6, 2)")
                    .HasColumnName("bpm");

                entity.Property(e => e.Chart).HasColumnName("chart");

                entity.Property(e => e.CustomMix).HasColumnName("customMix");

                entity.Property(e => e.DateCreation)
                    .HasColumnType("smalldatetime")
                    .HasColumnName("dateCreation")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Multitracks).HasColumnName("multitracks");

                entity.Property(e => e.Patches).HasColumnName("patches");

                entity.Property(e => e.ProPresenter).HasColumnName("proPresenter");

                entity.Property(e => e.RehearsalMix).HasColumnName("rehearsalMix");

                entity.Property(e => e.SongSpecificPatches).HasColumnName("songSpecificPatches");

                entity.Property(e => e.TimeSignature)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("timeSignature");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
