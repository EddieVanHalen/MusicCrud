namespace WebApplication1.DataAccess.Entities;

public class ArtistEntity
{
    public ArtistEntity() { }
    
    public ArtistEntity(int id, string name, string logoUrl)
    {
        Id = id;
        Name = name;
        LogoUrl = logoUrl;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string LogoUrl { get; set; } = null!;

    public virtual ICollection<AlbumEntity> Albums { get; set; } = null!;
}
