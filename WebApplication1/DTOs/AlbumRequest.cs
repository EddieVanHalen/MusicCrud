namespace WebApplication1.DTOs;

public class AlbumRequest
{
    public int Id { get; set; }
    public string Artist { get; set; } = String.Empty;
    public string Title { get; set; } = String.Empty;
    public string ImageUrl { get; set; } = String.Empty;
}