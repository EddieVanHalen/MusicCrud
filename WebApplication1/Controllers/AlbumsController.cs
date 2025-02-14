using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.DTOs.ForView;
using WebApplication1.Models.Abstractions.Repository;
using WebApplication1.Models.Models;

namespace WebApplication1.Controllers;

public class AlbumsController : Controller
{
    private readonly ILogger<AlbumsController> _logger;

    private readonly IAlbumsRepository _albumsRepository;
    private readonly IArtistRepository _artistsRepository;

    public AlbumsController(ILogger<AlbumsController> logger, IAlbumsRepository albumsRepository,
        IArtistRepository artistsRepository)
    {
        _logger = logger;
        _albumsRepository = albumsRepository;
        _artistsRepository = artistsRepository;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<Album> albums = await _albumsRepository.GetAllAlbumsAsync();

        return View(albums);
    }

    [HttpGet]
    public IActionResult AddAlbum()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        Album album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album.Id == 0)
        {
            TempData["danger"] = "Album Not Found";
            return RedirectToAction(nameof(Index));
        }

        Artist artist = await _artistsRepository.GetArtistByIdAsync(album.ArtistId);

        if (artist.Id == 0)
        {
            TempData["danger"] = "Artist Not Found";
            return RedirectToAction(nameof(Index));
        }

        AlbumDTO data = new AlbumDTO
        {
            Id = album.Id,
            Artist = artist.Name,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
        };

        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Album album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album.Id == 0)
        {
            TempData["danger"] = "Album not found";
            return RedirectToAction(nameof(Index));
        }

        return View(album);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(int id)
    {
        int result = await _albumsRepository.DeleteAlbumByIdAsync(id);

        if (result == 0)
        {
            TempData["danger"] = "Album wasn't deleted";
            _logger.LogInformation($"Album wasn't deleted {id}");
            return RedirectToAction("Index");
        }
        
        TempData["success"] = "Album was deleted";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAction(AlbumDTO request)
    {
        List<Album> albums = await _albumsRepository.GetAllAlbumsAsync();

        if (albums.FirstOrDefault(x => x.Title == request.Title) is not null)
        {
            TempData["danger"] = "Title already exists";
            return RedirectToAction(nameof(Update));
        }
        
        if (!ModelState.IsValid)
        {
            TempData["danger"] = "Invalid request";
            return RedirectToAction(nameof(Update));
        }

        Artist artist = await _artistsRepository.GetArtistByNameAsync(request.Artist.ToLower());

        if (artist.Id == 0)
        {
            TempData["danger"] = "Artist not found";
            return RedirectToAction(nameof(Update));
        }

        (Album album, ICollection<string> errors) =
            Album.Create(request.Id, artist.Id, request.Title, request.ImageUrl);

        if (errors.Any())
        {
            TempData["danger"] = "Invalid request";
            return RedirectToAction(nameof(Update));
        }

        int result = await _albumsRepository.UpdateAlbumAsync(album);

        if (result == 0)
        {
            TempData["danger"] = "Album wasn't updated";
            _logger.LogError($"Album wasn't updated {request.Id}");
            return RedirectToAction(nameof(Update));
        }
        
        TempData["success"] = "Album was updated";
        return RedirectToAction(nameof(Index));
    }


    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Album album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album.Id == 0)
        {
            TempData["danger"] = "Album wasn't found";
            return RedirectToAction(nameof(Index));
        }

        Artist artist = await _artistsRepository.GetArtistByIdAsync(album.ArtistId);

        if (artist.Id == 0)
        {
            TempData["danger"] = "Artist not found";
            return RedirectToAction(nameof(Index));
        }

        AlbumDTO data = new AlbumDTO
        {
            Id = album.Id,
            Artist = artist.Name,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
        };

        return View(data);
    }

    [HttpPost] 
    public async Task<IActionResult> AddAlbumAction(AlbumRequest request)
    {
        Artist artist = await _artistsRepository.GetArtistByNameAsync(request.Artist.ToLower());

        if (artist.Id == 0)
        {
            TempData["danger"] = "Artist not found";
            return RedirectToAction(nameof(AddAlbum));
        }

        (Album album, IEnumerable<string> errors) = Album.Create(0, artist.Id, request.Title, request.ImageUrl);

        if (errors.Any())
        {
            TempData["danger"] = "Invalid request";
            return RedirectToAction(nameof(AddAlbum));
        }

        int result = await _albumsRepository.AddAlbumAsync(album);
        
        if (result == 0)
        {
            TempData["danger"] = "Album wasn't added";
            _logger.LogError($"Album wasn't added {request.Title}");
            return RedirectToAction(nameof(AddAlbum));
        }
    
        TempData["success"] = "Album was added";
        return RedirectToAction(nameof(Index));
    }
}