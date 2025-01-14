﻿using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicAPI.Models
{
    public class Songs
    {
        
        public int Id { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Language { get; set; }
        public DateTime UploadDate { get; set; }
        public bool IsFeatured { get; set; }
        [NotMapped]
        public FormFile Image { get; set; }
        [NotMapped]
        public FormFile AudioFile { get; set; }
        public string ImageUrl { get; set; }
        public string AudioUrl { get; set; }

        public int ArtistId { get; set; }
        public int? AlbumId { get; set; }
    }
}
