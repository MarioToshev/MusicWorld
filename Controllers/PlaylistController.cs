using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicWorld.Data;
using MusicWorld.Models;
using MusicWorld.Service;
using MusicWorld.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicWorld.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IMapper mapper;
        private readonly MusicContext _context;
        private readonly IPlaylistService playlistService;
        private readonly ISongService songService;


        public PlaylistController(IMapper mapper, MusicContext context, IPlaylistService playlistService, ISongService songService)
        {
            this.mapper = mapper;
            this._context = context;
            this.playlistService = playlistService;
            this.songService = songService;
        }
        
        public async Task<IActionResult> CreatePlaylist()
        {
            ICollection<SongViewModel> a =  songService.GetAllSongs().Result.Select(s => mapper.Map<SongViewModel>(s)).ToList();
            return View(a);
        }
       
       
    }
}
