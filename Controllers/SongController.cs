using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Models;
using MusicWorld.Service.Interfaces;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    public class SongController : Controller
    {
        private readonly IMapper mapper;
        private readonly MusicContext _context;
        private readonly ISongService songService;

        public SongController(IMapper mapper, MusicContext context, ISongService songService)
        {
            this.mapper = mapper;
            this._context = context;
            this.songService = songService;
        }
        [HttpGet]
        public async Task<IActionResult> CreateSong(string albumName)
        {
            TempData["Album name"] = albumName;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateSong(SongViewModel model)
        {
            await songService.CreateSong(model, TempData["Album name"].ToString());
            return RedirectToAction("AllArtists", "Artist");
        }
        public async Task<IActionResult> DeleteSong(string songName)
        {
            await songService.DeleteSong(songName);
            return RedirectToAction("AllArtists", "Artist");
        }
        [HttpGet]
        public async Task<IActionResult> EditSong(string songName)
        {
            TempData["Song Name"] = songName;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditSong(SongViewModel model)
        {
            await songService.EditSong(model, TempData["Song Name"].ToString());
            TempData.Clear();
            return RedirectToAction("AllArtists", "Artist");
        }
        public async Task<IActionResult> AllSongs()
        {
            return View(await songService.GetAllSongs());
        }
        public async Task<IActionResult> DetailsSong(string songName)
        {
            return View(mapper.Map<SongViewModel>(await songService.GetSongByName(songName)));
        }


    }
}
