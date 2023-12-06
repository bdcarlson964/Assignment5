using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MvcMusic.Models;

public class SongGenreViewModel
{
    public List<Song>? Songs { get; set; }
    public SelectList? Genres { get; set; }
    public string? SongGenre { get; set; }
    public string? SongPerformer { get; set; }
    public SelectList? Performers { get; set; }
}