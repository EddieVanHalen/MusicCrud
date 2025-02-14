using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.DataAccess.Repository;

public class AlbumsRepository : IAlbumsRepository
{
    private readonly MusicDbContext _dbContext;

    private readonly ILogger<AlbumsRepository> _logger;

    public AlbumsRepository(MusicDbContext dbContext, ILogger<AlbumsRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<Album>> GetAllAlbumsAsync()
    {
        try
        {
            List<AlbumEntity> albumEntities = await _dbContext
                .Albums.AsNoTracking()
                .ToListAsync();

            return albumEntities.Select(a => Album.Create(a.Id, a.ArtistId, a.Title, a.ImageUrl).album).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching all albums : {ex.Message}");
            return new List<Album>();
        }
    }

    public async Task<Album> GetAlbumByIdAsync(int id)
    {
        try
        {
            AlbumEntity? albumEntity = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Id == id);

            if (albumEntity is null)
            {
                return new Album();
            }

            return Album.Create(albumEntity.Id, albumEntity.ArtistId, albumEntity.Title, albumEntity.ImageUrl).album;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching album by id : {ex.Message}");
            return new Album();
        }
    }

    public async Task<Album> GetAlbumByNameAsync(string title)
    {
        try
        {
            AlbumEntity? albumEntity = await _dbContext.Albums.FirstOrDefaultAsync(x => x.Title == title);

            if (albumEntity is null)
            {
                return new Album();
            }

            return Album.Create(albumEntity.Id, albumEntity.ArtistId, albumEntity.Title, albumEntity.ImageUrl).album;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching album by name : {ex.Message}");
            return new Album();
        }
    }

    public async Task<int> AddAlbumAsync(Album album)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while adding album : {ex.Message}");
            return 0;
        }
    }

    public async Task<int> UpdateAlbumAsync(Album album)
    {
        try
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
        catch (Exception e)
        {
            _logger.LogError(e, $"Error occurred while updating album : {e.Message}");
            return 0;
        }
    }

    public async Task<int> DeleteAlbumByIdAsync(int id)
    {
        try
        {
            await _dbContext.Albums.Where(x => x.Id == id).ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return id;
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error occurred while deleting album : {e.Message}");
            return 0;
        }
    }
}