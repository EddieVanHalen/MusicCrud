using Microsoft.EntityFrameworkCore;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Models;
using WebApplication1.Models.Abstractions.Repository;

namespace WebApplication1.DataAccess.Repository;

public class ArtistRepository : IArtistRepository
{
    private readonly MusicDbContext _dbContext;

    public ArtistRepository(MusicDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
    {
        IEnumerable<ArtistEntity> artistEntities = await _dbContext
            .Artists.AsNoTracking()
            .ToListAsync();

        return artistEntities.Select(a => Artist.Create(a.Id, a.Name).artist);
    }

    public async Task<Artist> GetArtistByIdAsync(int id)
    {
        ArtistEntity? artistEntity = await _dbContext.Artists.FirstOrDefaultAsync(a => a.Id == id);

        if (artistEntity is null)
        {
            return null;
        }

        return Artist.Create(artistEntity.Id, artistEntity.Name).artist;
    }

    public async Task<int> AddArtistAsync(Artist artist)
    {
        ArtistEntity artistEntity = new ArtistEntity
        {
            Name = artist.Name,
        };

        await _dbContext.Artists.AddAsync(artistEntity);
        await _dbContext.SaveChangesAsync();

        return artistEntity.Id;
    }

    public async Task<int> UpdateArtistAsync(Artist artist)
    {
        await _dbContext
            .Artists.Where(a => a.Id == artist.Id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Name, artist.Name));

        await _dbContext.SaveChangesAsync();

        return artist.Id;
    }

    public async Task<int> DeleteArtistByIdAsync(int id)
    {
        await _dbContext.Artists.Where(x => x.Id == id).ExecuteDeleteAsync();

        await _dbContext.SaveChangesAsync();

        return id;
    }
}
