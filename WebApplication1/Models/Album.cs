using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Album
{
    public int Id { get; set; }
    
    public int ArtistId { get; set; }
    
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    
    public DateTime ReleaseDate { get; set; } = DateTime.Now;
}