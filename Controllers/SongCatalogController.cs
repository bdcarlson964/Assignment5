using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMusic.Data;
using MvcMusic.Models;

namespace MvcMusic.Controllers
{
    public class SongCatalogController : Controller
    {
        private readonly MvcMusicContext _context;

        public SongCatalogController(MvcMusicContext context)
        {
            _context = context;
        }

        // GET: SongCatalog
        public async Task<IActionResult> Index(string songGenre, string songPerformer)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'MvcMusicContext.Song'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in _context.Song
                                            orderby m.Genre
                                            select m.Genre;

            // Use LINQ to get list of performers.
            IQueryable<string> performerQuery = from m in _context.Song
                                            orderby m.Performer
                                            select m.Performer;

            var songs = from m in _context.Song
                         select m;


            if (!string.IsNullOrEmpty(songGenre))
            {
                songs = songs.Where(x => x.Genre == songGenre);
            }

            if (!string.IsNullOrEmpty(songPerformer))
            {
                songs = songs.Where(x => x.Performer == songPerformer);
            }

            var songGenreVM = new SongGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Performers = new SelectList(await performerQuery.Distinct().ToListAsync()),
                Songs = await songs.ToListAsync()
            };

            return View(songGenreVM);
        }


        // GET: SongCatalog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Song
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

    }
}
