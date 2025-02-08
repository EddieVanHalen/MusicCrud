namespace WebApplication1.DataAccess.Entities;

public class AlbumEntity
{
    public AlbumEntity(int id, int artistId, string title)
    {
        Id = id;
        ArtistId = artistId;
        Title = title;
    }

    public int Id { get; set; }

    public int ArtistId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    public virtual ArtistEntity Artist { get; set; } = null!;
}
