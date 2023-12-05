﻿using System.ComponentModel.DataAnnotations;

namespace MvcMusic.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Performer { get; set; }
        public decimal Price { get; set; }
    }
}
