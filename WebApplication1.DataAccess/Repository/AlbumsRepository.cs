using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Models;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.DataAccess.Repository;

public class AlbumsRepository : IAlbumsRepository
{
    private readonly MusicDbContext _dbContext;

    public AlbumsRepository(MusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        List<AlbumEntity> albumEntities = await _dbContext
            .Albums.AsNoTracking()
            .ToListAsync();

        return albumEntities.Select(a => Album.Create(a.Id, a.ArtistId, a.Title, a.ImageUrl).album).ToList();
    }

    public async Task<Album?> GetAlbumByIdAsync(int id)
    {
        AlbumEntity? albumEntity = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);

        if (albumEntity is null)
        {
            return null;
        }
        
        return Album.Create(albumEntity.Id, albumEntity.ArtistId, albumEntity.Title, albumEntity.ImageUrl).album;
    }

    public async Task<int> AddAlbumAsync(Album album)
    {
        AlbumEntity albumEntity = new AlbumEntity
        {
            ArtistId = album.ArtistId,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
        };

        await _dbContext.Albums.AddAsync(albumEntity);
        await _dbContext.SaveChangesAsync();

        return albumEntity.Id;
    }

    public async Task<int> UpdateAlbumAsync(Album album)
    {
        await _dbContext
            .Albums
            .Where(a => a.Id == album.Id)
            .ExecuteUpdateAsync(setters => setters
                .SetProperty(x => x.Title, album.Title)
                .SetProperty(x => x.ImageUrl, album.ImageUrl)
                .SetProperty(x => x.ArtistId, album.ArtistId));

        await _dbContext.SaveChangesAsync();

        return album.Id;
    }

    public async Task<int> DeleteAlbumByIdAsync(int id)
    {
        await _dbContext.Albums.Where(x => x.Id == id).ExecuteDeleteAsync();

        await _dbContext.SaveChangesAsync();

        return id;
    }
}