using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Highway_to_heaven.Models
{
    public partial class HIGHWAY_TO_HEAVENContext : DbContext
    {
        public HIGHWAY_TO_HEAVENContext()
        {
        }

        public HIGHWAY_TO_HEAVENContext(DbContextOptions<HIGHWAY_TO_HEAVENContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentRating> CommentRatings { get; set; }
        public virtual DbSet<PackageTour> PackageTours { get; set; }
        public virtual DbSet<Planet> Planets { get; set; }
        public virtual DbSet<Travel> Travels { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=HIGHWAY_TO_HEAVEN;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("COMMENT_PK");

                entity.Property(e => e.IdComment).ValueGeneratedNever();

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdTour)
                    .HasConstraintName("COMMENT_PACKAGE_TOUR_FK");

                entity.HasOne(d => d.IdTravelerNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdTraveler)
                    .HasConstraintName("COMMENT_TRAVELER_FK");
            });

            modelBuilder.Entity<CommentRating>(entity =>
            {
                entity.Property(e => e.RatingId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdCommentNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdComment)
                    .HasConstraintName("FK__COMMENT_R__id_co__4BAC3F29");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.IdUser)
                    .HasConstraintName("COMMENTRAT_TRAVELER_FK");
            });

            modelBuilder.Entity<PackageTour>(entity =>
            {
                entity.HasKey(e => e.IdTour)
                    .HasName("PACKAGE_TOUR_PK");

                entity.HasOne(d => d.PlanetNameNavigation)
                    .WithMany(p => p.PackageTours)
                    .HasForeignKey(d => d.PlanetName)
                    .HasConstraintName("PLANET_FK");
            });

            modelBuilder.Entity<Planet>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PLANET_PK");
            });

            modelBuilder.Entity<Travel>(entity =>
            {
                entity.HasKey(e => e.IdTravel)
                    .HasName("TRAVEL_PK");

                entity.HasOne(d => d.IdTourNavigation)
                    .WithMany(p => p.Travels)
                    .HasForeignKey(d => d.IdTour)
                    .HasConstraintName("PACKAGE_TOUR_FK");

                entity.HasOne(d => d.IdTravelerNavigation)
                    .WithMany(p => p.Travels)
                    .HasForeignKey(d => d.IdTraveler)
                    .HasConstraintName("TRAVELER_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("TRAVELER_PK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
