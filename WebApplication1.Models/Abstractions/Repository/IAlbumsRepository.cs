using WebApplication1.Models.Models;

namespace WebApplication1.Models.Abstractions.Repository;

public interface IAlbumsRepository
{
    Task<List<Album>> GetAllAlbumsAsync();
    Task<Album?> GetAlbumByIdAsync(int id);
    Task<int> AddAlbumAsync(Album album);
    Task<int> UpdateAlbumAsync(Album album);
    Task<int> DeleteAlbumByIdAsync(int id);
}