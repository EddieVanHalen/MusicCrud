using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Artist
{
    private const int MINIMUM_NAME_LENGTH = 5;

    private Artist(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; private set; }

    [MinLength(5), MaxLength(250)]
    public string Name { get; private set; } = null!;

    public static (Artist artist, ICollection<string> errors) Create(int id, string name)
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Name is null or white space.");
        }

        if (name.Length < MINIMUM_NAME_LENGTH)
        {
            errors.Add("Name must be at least 5 characters long.");
        }

        Artist artist = new Artist(id, name);

        return (artist, errors);
    }
}
