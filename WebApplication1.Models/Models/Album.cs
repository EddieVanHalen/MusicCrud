using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Album
{
    private const int TITLE_MINIMUM_LENGTH = 5;

    private Album(int id, int artistId, string title, string imageUrl)
    {
        Id = id;
        ArtistId = artistId;
        Title = title;
        ImageUrl = imageUrl;
    }

    public int Id { get; private set; }

    public int ArtistId { get; private set; }

    [MaxLength(100)]
    public string Title { get; private set; } = null!;

    public DateTime ReleaseDate { get; private set; } = DateTime.Now;

    public string ImageUrl { get; private set; } = null!;

    public static (Album album, ICollection<string> errors) Create(
        int id,
        int artistId,
        string title,
        string imageUrl
    )
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrEmpty(title))
        {
            errors.Add("Title is null");
        }

        if (string.IsNullOrEmpty(imageUrl))
        {
            errors.Add("Image Url is null");
        }

        if (title.Length < TITLE_MINIMUM_LENGTH)
        {
            errors.Add("Title Must Be At Least 5 symbols.");
        }

        Album album = new Album(id, artistId, title, imageUrl);

        return (album, errors);
    }
}
