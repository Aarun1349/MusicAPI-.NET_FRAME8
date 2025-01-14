﻿using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicAPI.Models
{
    public class Artists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [NotMapped]
        public FormFile Image { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Album> Albums { get; set; }
        public ICollection<Songs> Songs { get; set; }
    }
}
