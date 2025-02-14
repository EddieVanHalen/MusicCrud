using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.DataAccess.Repository;

public class ArtistRepository : IArtistRepository
{
    private readonly MusicDbContext _dbContext;

    private readonly ILogger<ArtistRepository> _logger;

    public ArtistRepository(MusicDbContext dbContext, ILogger<ArtistRepository> logger)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<List<Artist>> GetAllArtistsAsync()
    {
        try
        {
            List<ArtistEntity> artistEntities = await _dbContext
                .Artists.AsNoTracking()
                .ToListAsync();

            return artistEntities.Select(a => Artist.Create(a.Id, a.Name, a.LogoUrl).artist).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching all artists : {ex.Message}");
            return new List<Artist>();
        }
    }

    public async Task<Artist> GetArtistByIdAsync(int id)
    {
        try
        {
            ArtistEntity? artistEntity = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Id == id);

            if (artistEntity is null)
            {
                return new Artist();
            }

            return Artist.Create(artistEntity.Id, artistEntity.Name, artistEntity.LogoUrl).artist;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting artist by id : {ex.Message}");
            return new Artist();
        }
    }

    public async Task<Artist> GetArtistByNameAsync(string name)
    {
        try
        {
            ArtistEntity? artistEntity = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Name.ToLower() == name);

            if (artistEntity is null)
            {
                return new Artist();
            }

            return Artist.Create(artistEntity.Id, artistEntity.Name, artistEntity.LogoUrl).artist;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while getting artist by name : {ex.Message}");
            return new Artist();
        }
    }

    public async Task<int> AddArtistAsync(Artist artist)
    {
        try
        {
            ArtistEntity artistEntity = new ArtistEntity
            {
                Name = artist.Name,
                LogoUrl = artist.LogoUrl
            };

            await _dbContext.Artists.AddAsync(artistEntity);
            await _dbContext.SaveChangesAsync();

            return artistEntity.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while adding artist : {ex.Message}");
            return 0;
        }
    }

    public async Task<int> UpdateArtistAsync(Artist artist)
    {
        try
        {
            await _dbContext
                .Artists
                .Where(a => a.Id == artist.Id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Name, artist.Name)
                    .SetProperty(x => x.LogoUrl, artist.LogoUrl));

            await _dbContext.SaveChangesAsync();

            return artist.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating artist : {ex.Message}");
            return 0;
        }
    }

    public async Task<int> DeleteArtistByIdAsync(int id)
    {
        try
        {
            await _dbContext.Artists.Where(x => x.Id == id).ExecuteDeleteAsync();

            await _dbContext.SaveChangesAsync();

            return id;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting artist : {ex.Message}");
            return 0;
        }
    }
}