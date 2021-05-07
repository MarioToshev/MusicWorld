using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Models;
using MusicWorld.Service.Interfaces;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IMapper mapper;
        private readonly MusicContext _context;
        private readonly IArtistService artistService;

        public ArtistController(IMapper mapper, MusicContext context, IArtistService artistService)
        {
            this.mapper = mapper;
            this._context = context;
            this.artistService = artistService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateArtist()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateArtist(ArtistViewModel model)
        {

            await artistService.CreateArtist(model);
            return RedirectToAction("AllArtists", "Artist");
        }
        public async Task<IActionResult> DeleteArtist(string artistName)
        {
            await artistService.DeleteArtist(artistName);
            return RedirectToAction("AllArtists", "Artist");
        }
        [HttpGet]
        public async Task<IActionResult> EditArtist(string artistName)
        {
            TempData["Artist Name"] = artistName;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditArtist(ArtistViewModel model)
        {
            await artistService.EditArtist(model, TempData["Artist Name"].ToString());
            TempData.Clear();
            return RedirectToAction("AllArtists", "Artist");
        }
        public async Task<IActionResult> AllArtists()
        {
            TempData.Clear();
            return View(await artistService.GetAllArtists());
        }
        public async Task<IActionResult> DetailsArtist(string artistName)
        {
            return View(mapper.Map<ArtistViewModel>(await artistService.GetArtistByName(artistName)));
        }


    }
}
