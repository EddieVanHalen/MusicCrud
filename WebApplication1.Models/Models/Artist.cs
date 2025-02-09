using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Models;

public class Artist
{
    private const int MINIMUM_NAME_LENGTH = 5;

    public Artist()
    {
        
    }

    private Artist(int id, string name, string logoUrl)
    {
        Id = id;
        Name = name;
        LogoUrl = logoUrl;
    }

    public int Id { get; private set; }

    [MinLength(5), MaxLength(250)]
    public string Name { get; private set; } = null!;
    
    [Required]
    public string LogoUrl { get; private set; } = null!;

    public static (Artist artist, ICollection<string> errors) Create(int id, string name, string logoUrl)
    {
        ICollection<string> errors = new List<string>();

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add("Name is null or white space.");
        }

        if (string.IsNullOrWhiteSpace(logoUrl))
        {
            errors.Add("Logo Url is null or white space.");
        }
        
        if (name.Length < MINIMUM_NAME_LENGTH)
        {
            errors.Add("Name must be at least 5 characters long.");
        }

        Artist artist = new Artist(id, name, logoUrl);

        return (artist, errors);
    }
}
