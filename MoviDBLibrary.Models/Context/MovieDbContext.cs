using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MoviDBLibrary.DataAccess.EF;
using MoviDBLibrary.DataAccess.EF.Models;
using MovieDBLibrary.DataAccess.EF.Models;

namespace MovieDBLibrary.DataAccess.EF;

public partial class MovieDbContext : DbContext
{
    public MovieDbContext()
    {
    }

    public MovieDbContext(DbContextOptions<MovieDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=2amLS\\SQLEXPRESS;Database=MovieDB;Trusted_Connection=True;TrustServerCertificate=True");


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC072D9F6F36");

            entity.Property(e => e.Genre1)
                .HasMaxLength(50)
                .HasColumnName("Genre");
            
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__Movies__4BD2941A81D2024F");

            entity.Property(e => e.Cast).HasMaxLength(200);
            entity.Property(e => e.Director).HasMaxLength(50);
            entity.Property(e => e.GrossRevenue).HasMaxLength(50);
            entity.Property(e => e.LeadActorActress)
                .HasMaxLength(50)
                .HasColumnName("LeadActor/Actress");
            entity.Property(e => e.MaturityRating).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(120);

            entity.HasOne(d => d.Genre).WithMany(p => p.Movies)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Movies_Genres");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4C01F92F48");

            entity.ToTable("User");

            entity.Property(e => e.EmailAddress).HasMaxLength(150);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(150);
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserListId).HasName("PK__UserList__374B941105665EF4");

            entity.HasOne(d => d.Movie).WithMany(p => p.UserLists)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__UserLists__Movie__681373AD");

            entity.HasOne(d => d.User).WithMany(p => p.UserLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserLists__UserI__671F4F74");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
