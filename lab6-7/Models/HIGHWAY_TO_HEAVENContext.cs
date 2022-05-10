using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace lab6_7.Models
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

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
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
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.HasKey(e => e.Login)
                    .HasName("ADMIN_PK");
            });

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
