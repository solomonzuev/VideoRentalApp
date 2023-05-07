using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace VideoRental.Models;

public partial class VideoRentalDbContext : DbContext
{
    private static VideoRentalDbContext _context;

    public VideoRentalDbContext()
    {
    }

    public VideoRentalDbContext(DbContextOptions<VideoRentalDbContext> options)
        : base(options)
    {
    }

    public static VideoRentalDbContext GetContext()
        => _context ??= new VideoRentalDbContext();

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<FilmCredit> FilmCredits { get; set; }

    public virtual DbSet<FilmsInMedia> FilmsInMedia { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<StoreLocation> StoreLocations { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.Customers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Customers__UserI__59C55456");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("FK_Employees_Positions");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK_Employees_StoreLocations");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Employees__UserI__6166761E");
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Videos");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Price3Days).HasColumnType("money");
            entity.Property(e => e.ReleaseDate).HasColumnType("date");

            entity.HasOne(d => d.Author).WithMany(p => p.FilmAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_VideoCelebrities");

            entity.HasOne(d => d.Director).WithMany(p => p.FilmDirectors)
                .HasForeignKey(d => d.DirectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_VideoCelebrities1");

            entity.HasOne(d => d.Genre).WithMany(p => p.Films)
                .HasForeignKey(d => d.GenreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Videos_Genres");

            entity.HasMany(d => d.Actors).WithMany(p => p.Films)
                .UsingEntity<Dictionary<string, object>>(
                    "ActorsInVideo",
                    r => r.HasOne<FilmCredit>().WithMany()
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ActorsInVideos_VideoParticipants"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ActorsInVideos_Videos"),
                    j =>
                    {
                        j.HasKey("FilmId", "ActorId");
                        j.ToTable("ActorsInVideos");
                    });
        });

        modelBuilder.Entity<FilmCredit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_VideoCelebrities");

            entity.Property(e => e.Birthdate).HasColumnType("date");
            entity.Property(e => e.Deathdate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Sex)
                .HasMaxLength(1)
                .IsFixedLength();
        });

        modelBuilder.Entity<FilmsInMedia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VideosIn__3214EC07143A6298");

            entity.HasIndex(e => new { e.FilmId, e.MediaTypeId }, "UQ__VideosIn__51C93937FCAF3C89").IsUnique();

            entity.Property(e => e.IsAvaliable)
                .IsRequired()
                .HasDefaultValueSql("((1))");

            entity.HasOne(d => d.Film).WithMany(p => p.FilmsInMedia)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VideosInM__Video__540C7B00");

            entity.HasOne(d => d.MediaType).WithMany(p => p.FilmsInMedia)
                .HasForeignKey(d => d.MediaTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VideosInM__Media__55009F39");

            entity.HasOne(d => d.Store).WithMany(p => p.FilmsInMedia)
                .HasForeignKey(d => d.StoreId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VideosInMedia_StoreLocations");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Media");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Position__3214EC07861CB707");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Salary).HasColumnType("money");
        });

        modelBuilder.Entity<StoreLocation>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.TotalPrice).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Customers");

            entity.HasOne(d => d.Film).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FilmId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_Videos");

            entity.HasOne(d => d.VideosInMedia).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.VideosInMediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Transactions_FilmsInMedia");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07ED95D65A");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
