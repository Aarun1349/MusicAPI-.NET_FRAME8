using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicAPI.Models
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ArtistId { get; set; }
        public ICollection<Songs> Songs { get; set; }
        
        [NotMapped]
        public FormFile Image { get; set; }
        public string ImageUrl { get; set; }
    }
}
