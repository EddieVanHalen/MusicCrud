namespace WebApplication1.DataAccess.Entities;

public class AlbumEntity
{
    public AlbumEntity()
    {
        
    }
    
    public AlbumEntity(int id, int artistId, string title, string imageUrl)
    {
        Id = id;
        ArtistId = artistId;
        Title = title;
        ImageUrl = imageUrl;
    }

    public int Id { get; set; }

    public int ArtistId { get; set; }
            
    public string Title { get; set; } = null!;

    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    public string ImageUrl { get; set; } = null!;

    public virtual ArtistEntity Artist { get; set; } = null!;
}
