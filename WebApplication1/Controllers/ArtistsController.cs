using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.Controllers;

public class ArtistsController : Controller
{
    private readonly IArtistRepository _artistRepository;

    public ArtistsController(IArtistRepository artistRepository)
    {
        _artistRepository = artistRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Artist> artists = await _artistRepository.GetAllArtistsAsync();

        return View(artists);
    }

    [HttpGet]
    public IActionResult AddArtist()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddArtistAction(string name, string logoUrl)
    {
        Artist artist = Artist.Create(0, name, logoUrl).artist;

        await _artistRepository.AddArtistAsync(artist);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAction(ArtistRequest request)
    {
        (Artist artist, IEnumerable<string> errors) = Artist.Create(request.Id, request.Name, request.LogoUrl);

        if (errors.Any())
        {
            return BadRequest(errors);
        }

        await _artistRepository.UpdateArtistAsync(artist);

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Artist? artist = await _artistRepository.GetArtistByIdAsync(id);

        if (artist is null)
        {
            return NotFound();
        }
        
        return View(artist);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(int id)
    {
        await _artistRepository.DeleteArtistByIdAsync(id);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Artist? artist = await _artistRepository.GetArtistByIdAsync(id);

        if(artist is null) return NotFound();

        return View(artist);
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Artist? artist = await _artistRepository.GetArtistByIdAsync(id);

        if (artist is null) return NotFound();

        return View(artist);
    }
}
