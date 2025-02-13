namespace WebApplication1.DTOs;

public class ArtistRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string LogoUrl { get; set; } = String.Empty;
}