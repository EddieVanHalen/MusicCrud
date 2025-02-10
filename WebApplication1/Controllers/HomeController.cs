using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public IActionResult Albums()
    {
        return View();
    }

    #region Add





    [HttpGet]
    public IActionResult AddAlbum()
    {
        return View();
    }

    #endregion
    
}
