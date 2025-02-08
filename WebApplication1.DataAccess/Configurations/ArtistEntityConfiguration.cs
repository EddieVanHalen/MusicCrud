using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.DataAccess.Entities;

namespace WebApplication1.DataAccess.Configurations;

public class ArtistEntityConfiguration : IEntityTypeConfiguration<ArtistEntity>
{
    public void Configure(EntityTypeBuilder<ArtistEntity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name);
        builder.HasIndex(x => x.Name).IsUnique(true);

        builder
            .HasMany<AlbumEntity>(x => x.Albums)
            .WithOne(x => x.Artist)
            .HasForeignKey(x => x.ArtistId);
    }
}
