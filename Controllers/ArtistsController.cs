using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Helpers;
using MusicAPI.Models;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public ArtistsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/<DbSongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Artists artist)
        {
            try
            {

                if (artist.Image == null || artist.Image.Length == 0)
                {
                    return BadRequest("No file uploaded.");
                }
                var imageUrl = await CloudinaryService.CloudinaryImageUpload(artist.Image);
                artist.ImageUrl = imageUrl;
                await _dbContext.AddAsync(artist);
                await _dbContext.SaveChangesAsync();

                var result = new
                {
                    success = true,
                    message="New Artist Added Successfully",
                    count = await _dbContext.Songs.CountAsync(),
                    artist = artist
                };

                return StatusCode(StatusCodes.Status201Created);
                //return Ok(result);
            }
            catch (Exception e)
            {
                var result = new
                {

                    success = true,
                    message = e.Message

                };
                return Ok(result);
            }

        }


        [HttpGet]
        public async Task<IActionResult> GetArtists(int? pageSize, int? pageNumber)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;
            var artists  =await (from artist in _dbContext.Artists
            select new
            {
                Id= artist.Id,
                Nama= artist.Name,
                ImageUrl = artist.ImageUrl,
            }).ToListAsync();

            return Ok(artists.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> ArtistDetails(int artistId)
        {

            var artistDetails = await  _dbContext.Artists.Where(a => a.Id == artistId).Include(a => a.Songs).ToListAsync();
            return Ok(artistDetails);
        }

    }
}
