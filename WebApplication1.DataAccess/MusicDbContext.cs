using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.DataAccess;

public class MusicDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    
    public DbSet<Album> Albums { get; set; }
    
    public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        base.OnModelCreating(modelBuilder);
    }
}