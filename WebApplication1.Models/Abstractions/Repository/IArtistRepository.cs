using WebApplication1.Models;
using WebApplication1.Models.Models;

namespace WebApplication1.Models.Abstractions.Repository;

public interface IArtistRepository
{
    Task<List<Artist>> GetAllArtistsAsync();
    Task<Artist?> GetArtistByIdAsync(int id);
    Task<int> AddArtistAsync(Artist artist);
    Task<int> UpdateArtistAsync(Artist artist);
    Task<int> DeleteArtistByIdAsync(int id);
}
