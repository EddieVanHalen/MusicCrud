using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.ForView;

public class AlbumDTO
{
    public int Id { get; set; }
    
    [MinLength(5)]
    public string Title { get; set; } = string.Empty;
    public string Artist { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}