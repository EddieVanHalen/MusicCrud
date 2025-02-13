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

    public AlbumsController(ILogger<AlbumsController> logger, IAlbumsRepository albumsRepository, IArtistRepository artistsRepository)
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
        Album? album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album is null)
        {
            return NotFound();
        }
        
        Artist? artist = await _artistsRepository.GetArtistByIdAsync(album.ArtistId);

        AlbumDTO data = new AlbumDTO
        {
            Id = album.Id,
            Artist = artist!.Name,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
        };
        
        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Album? album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album is null)
        {
            return NotFound();
        }
        
        return View(album);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAction(int id)
    {
        await _albumsRepository.DeleteAlbumByIdAsync(id);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateAction(AlbumDTO request)
    {
        Artist? artist = await _artistsRepository.GetArtistByNameAsync(request.Artist.ToLower());

        if (artist is null)
        {
            return NotFound("Artist not found");
        }
        
        Album? album = Album.Create(request.Id ,artist.Id, request.Title, request.ImageUrl).album;
        
        await _albumsRepository.UpdateAlbumAsync(album);
        
        return RedirectToAction(nameof(Index));
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Album? album = await _albumsRepository.GetAlbumByIdAsync(id);

        if (album is null)
        {
            return NotFound();
        }
        
        Artist? artist = await _artistsRepository.GetArtistByIdAsync(album.ArtistId);

        AlbumDTO data = new AlbumDTO
        {
            Id = album.Id,
            Artist = artist!.Name,
            Title = album.Title,
            ImageUrl = album.ImageUrl,
        };
        
        return View(data);
    }

    [HttpPost]
    public async Task<IActionResult> AddAlbumAction(AlbumRequest request)
    {
        Artist? artist = await _artistsRepository.GetArtistByNameAsync(request.Artist.ToLower());

        if (artist is null)
        {
            return BadRequest("Artist not found");
        }

        (Album album, IEnumerable<string> errors) = Album.Create(0, artist.Id, request.Title, request.ImageUrl);

        if (errors.Any())
        {
            return BadRequest(errors);
        }
        
        await _albumsRepository.AddAlbumAsync(album);
        
        return RedirectToAction(nameof(Index));
    }
}
