using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataAccess.Entities;

namespace WebApplication1.DataAccess.Configurations;

public class AlbumEntityConfiguration : IEntityTypeConfiguration<AlbumEntity>
{
    public void Configure(EntityTypeBuilder<AlbumEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title);
        builder.HasIndex(x => x.Title).IsUnique(true);
        builder.Property(x => x.ReleaseDate);
        builder.Property(x => x.);
    }
}
