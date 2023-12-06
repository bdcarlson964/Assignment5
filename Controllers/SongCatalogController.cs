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
        
        // GET: genre
        public async Task<IActionResult> Index(string songsGenre, string performers)
        {
            if (_context.Song == null)
            {
                return Problem("Entity set 'MvcMusicContext.Song'  is null.");
            }

            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from g in _context.Song
                                            orderby g.Genre
                                            select g.Genre;

            IQueryable<string> performerQuery = from g in _context.Song
                                                orderby g.Performer
                                                select g.Performer;
            var songs = from g in _context.Song
                        select g;


            if (!string.IsNullOrEmpty(songsGenre))
            {
                songs = songs.Where(s => s.Genre == songsGenre);
            }

            if (!string.IsNullOrEmpty(performers))
            {
                songs = songs.Where(p => p.Performer == performers);
            }

            var songsGenreVM = new MusicGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Performers = new SelectList(await performerQuery.Distinct().ToListAsync()),
                Songs = await songs.ToListAsync()
            };

            return View(songsGenreVM);
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
