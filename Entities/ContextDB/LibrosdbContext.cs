using System;
using System.Collections.Generic;
using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Entities.ContextDB;

public partial class LibrosdbContext : DbContext
{
    public LibrosdbContext()
    {
    }

    public LibrosdbContext(DbContextOptions<LibrosdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Book { get; set; }

    public virtual DbSet<Review> Review { get; set; }

    public virtual DbSet<Users> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=WISMANE;DataBase=LIBROSDB;Integrated Security=true; TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BOOK__3214EC2751EEE043");

            entity.ToTable("BOOK");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Author).HasMaxLength(50);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Descriptions).HasColumnType("text");
            entity.Property(e => e.Img).HasMaxLength(100);
            entity.Property(e => e.Rute).HasMaxLength(100);
            entity.Property(e => e.Title).HasMaxLength(50);
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__REVIEW__3214EC07E9D9B330");

            entity.ToTable("REVIEW");

            entity.Property(e => e.Descriptions).HasColumnType("text");

            entity.HasOne(d => d.IdBookNavigation).WithMany(p => p.Review)
                .HasForeignKey(d => d.IdBook)
                .HasConstraintName("FK__REVIEW__IdBook__48CFD27E");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Review)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__REVIEW__IdUser__49C3F6B7");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__USERS__3214EC07C74E3A36");

            entity.ToTable("USERS");

            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Img).HasMaxLength(100);
            entity.Property(e => e.Pass).HasMaxLength(50);
            entity.Property(e => e.Rute).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
