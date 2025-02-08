using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Album
{
    private const int TITLE_MINIMUM_LENGTH = 5;

    private Album(int id, int artistId, string title)
    {
        Id = id;
        ArtistId = artistId;
        Title = title;
    }

    public int Id { get; set; }

    public int ArtistId { get; set; }

    [MaxLength(100)]
    public string Title { get; set; } = null!;

    public DateTime ReleaseDate { get; set; } = DateTime.Now;

    public static (Album album, ICollection<string> errors) Create(
        int id,
        int artistId,
        string title
    )
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrEmpty(title))
        {
            errors.Add("Title is null");
        }

        if (title.Length < TITLE_MINIMUM_LENGTH)
        {
            errors.Add("Title Must Be At Least 5 symbols.");
        }

        Album album = new Album(id, artistId, title);

        return (album, errors);
    }
}
