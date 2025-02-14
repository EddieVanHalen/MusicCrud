using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.Controllers;

public class ArtistsController : Controller
{
    private readonly IArtistRepository _artistRepository;
    private readonly ILogger<ArtistsController> _logger;

    public ArtistsController(IArtistRepository artistRepository, ILogger<ArtistsController> logger)
    {
        _artistRepository = artistRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Artist> artists = await _artistRepository.GetAllArtistsAsync();
        
        _logger.LogInformation($"Retrieved {artists.Count} artists");
        return View(artists);
    }

    [HttpGet]
    public IActionResult AddArtist()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddArtistAction(ArtistRequest artistRequest)
    {
        Artist artist = Artist.Create(0, artistRequest.Name, artistRequest.LogoUrl).artist;

        int result = await _artistRepository.AddArtistAsync(artist);

        if (result == 0)
        {
            TempData["danger"] = "Artist wasn't added";
            _logger.LogError($"Artist wasn't added {artistRequest.Name}");
            return RedirectToAction(nameof(Index));
        }
        
        TempData["success"] = "Artist was added successfully";
        _logger.LogInformation($"Artist was added {artistRequest.Name}");
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAction(ArtistRequest request)
    {
        (Artist artist, ICollection<string> errors) = Artist.Create(request.Id, request.Name, request.LogoUrl);

        if (errors.Any())
        {
            TempData["danger"] = string.Join("; ", errors);
            return BadRequest(errors);
        }

        int result = await _artistRepository.UpdateArtistAsync(artist);

        if (result == 0)
        {
            TempData["danger"] = "Artist wasn't updated";
            _logger.LogError($"Artist wasn't updated {artist.Name}");
            return RedirectToAction(nameof(Index));
        }
        
        TempData["success"] = "Artist was updated successfully";
        _logger.LogInformation($"Artist was updated {artist.Name}");
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Artist artist = await _artistRepository.GetArtistByIdAsync(id);

        if (artist.Id == 0)
        {
            return NotFound("Artist was not found");
        }
        
        return View(artist);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(int id)
    {
        int result = await _artistRepository.DeleteArtistByIdAsync(id);

        if (result == 0)
        {
            TempData["danger"] = "Artist wasn't deleted";
            _logger.LogError($"Artist wasn't deleted {id}");
            return RedirectToAction(nameof(Index));
        }
        
        TempData["success"] = "Artist was deleted successfully";
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Artist artist = await _artistRepository.GetArtistByIdAsync(id);

        if (artist.Id == 0)
        {
            return NotFound("Artist wasn't found");
        }

        return View(artist);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Artist artist = await _artistRepository.GetArtistByIdAsync(id);

        if (artist.Id == 0)
        {
            return NotFound("Artist wasn't found");
        }

        return View(artist);
    }
}
