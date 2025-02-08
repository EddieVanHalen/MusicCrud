using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Configurations;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Models;

namespace WebApplication1.DataAccess;

public class MusicDbContext : DbContext
{
    public DbSet<ArtistEntity> Artists { get; set; } = null!;

    public DbSet<AlbumEntity> Albums { get; set; } = null!;

    public MusicDbContext(DbContextOptions<MusicDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        new AlbumEntityConfiguration().Configure(modelBuilder.Entity<AlbumEntity>());
        new ArtistEntityConfiguration().Configure(modelBuilder.Entity<ArtistEntity>());

        base.OnModelCreating(modelBuilder);
    }
}
