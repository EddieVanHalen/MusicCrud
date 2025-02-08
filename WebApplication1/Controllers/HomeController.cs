using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.Abstractions.Repository;

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

    public IActionResult Index()
    {
        return View();
    }

    #region Add

    [HttpPost]
    public async Task<IActionResult> AddArtistAction(string name)
    {
        await _artistRepository.AddArtistAsync(Artist.Create(-1, name).artist);
        
        return RedirectToAction(nameof(Index));
    }

    #endregion


    #region Get

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
}
