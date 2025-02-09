using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly IArtistRepository _artistRepository;

    public HomeController(ILogger<HomeController> logger, IArtistRepository artistRepository)
    {
        _logger = logger;
        _artistRepository = artistRepository;
    }

    public async Task<IActionResult> Artists()
    {
        IE<Artist> artists = await _artistRepository.GetAllArtistsAsync();
        
        return View();
    }

    public IActionResult Albums()
    {
        return View();
    }

    #region Add

    [HttpPost]
    public async Task<IActionResult> AddArtistAction(string name, string logoUrl)
    {
        await _artistRepository.AddArtistAsync(Artist.Create(-1, name, logoUrl).artist);

        return RedirectToAction(nameof(Artists));
    }

    [HttpGet]
    public IActionResult AddArtist()
    {
        return View();
    }

    [HttpGet]
    public IActionResult AddAlbum()
    {
        return View();
    }

    #endregion

    #region Update

    [HttpGet]
    public IActionResult UpdateArtist(int id)
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> UpdateArtistAction(Artist artist)
    {
        await _artistRepository.UpdateArtistAsync(artist);

        return RedirectToAction(nameof(Artists));
    }

    #endregion
}