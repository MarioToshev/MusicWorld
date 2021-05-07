using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Models;
using MusicWorld.Service.Interfaces;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    public class AlbumController : Controller
    {
        private readonly IMapper mapper;
        private readonly MusicContext _context;
        private readonly IAlbumService artistService;

        public AlbumController(IMapper mapper, MusicContext context, IAlbumService artistService)
        {
            this.mapper = mapper;
            this._context = context;
            this.artistService = artistService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateAlbum(string artistName)
        {
            TempData["Artist name"] = artistName;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAlbum(AlbumViewModel model)
        {
            await artistService.CreateAlbum(model, TempData["Artist name"].ToString());
            return RedirectToAction("AllArtists", "Artist");
        }
        public async Task<IActionResult> DeleteAlbum(string artistName)
        {
            await artistService.DeleteAlbum(artistName);
            return RedirectToAction("AllArtists", "Artist");
        }
        [HttpGet]
        public async Task<IActionResult> EditAlbum(string artistName)
        {
            TempData["Album Name"] = artistName;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditAlbum(AlbumViewModel model)
        {
            await artistService.EditAlbum(model, TempData["Album Name"].ToString());
            TempData.Clear();
            return RedirectToAction("AllAlbums", "Album");
        }
        public async Task<IActionResult> AllAlbums()
        {
            return View(await artistService.GetAllAlbums());
        }
        public async Task<IActionResult> DetailsAlbum(string artistName)
        {
            return View(mapper.Map<AlbumViewModel>(await artistService.GetAlbumByName(artistName)));
        }


    }
}
