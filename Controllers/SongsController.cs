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
    public class SongsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public SongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // POST api/<DbSongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Songs song)
        {
            try
            {
                var imageUrl = await CloudinaryService.CloudinaryImageUpload(song.Image);
                var audioUrl = await CloudinaryService.CloudinaryAudioUpload(song.AudioFile);
                song.ImageUrl = imageUrl;
                song.AudioUrl = audioUrl;
                await _dbContext.AddAsync(song);
                await _dbContext.SaveChangesAsync();

                var result = new
                {
                    success = true,
                    message = "New Song Added Successfully",
                    count = await _dbContext.Songs.CountAsync(),
                    song = song
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
        public async Task<IActionResult> GetAllSongs(int? pageSize, int? pageNumber)
        {
           int currentPageNumber =  pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;
            var songs = await (from song in _dbContext.Songs
                                 select new
                                 {
                                     Id = song.Id,
                                     Title = song.Title,
                                     ImageUrl = song.ImageUrl,
                                     Duration = song.Duration,
                                     AudioUrl = song.AudioUrl,
                                 }).ToListAsync();

            return Ok(songs.Skip((currentPageNumber-1)*currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> FeaturedSongs()
        {
            var songs = await (from song in _dbContext.Songs where song.IsFeatured == true
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   ImageUrl = song.ImageUrl,
                                   Duration = song.Duration,
                                   AudioUrl = song.AudioUrl,
                               }).ToListAsync();

            return Ok(songs);
        }


        [HttpGet("[action]")]
        public async Task<IActionResult> NewSongs()
        {
            var songs = await (from song in _dbContext.Songs
                               orderby song.UploadDate descending
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   ImageUrl = song.ImageUrl,
                                   Duration = song.Duration,
                                   AudioUrl = song.AudioUrl,
                               }).Take(15).ToListAsync();

            return Ok(songs);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SeachSongs(string query)
        {
            var songs = await (from song in _dbContext.Songs
                               where song.Title.StartsWith(query)
                               select new
                               {
                                   Id = song.Id,
                                   Title = song.Title,
                                   ImageUrl = song.ImageUrl,
                                   Duration = song.Duration,
                                   AudioUrl = song.AudioUrl,
                               }).Take(15).ToListAsync();

            return Ok(songs);
        }
    }
}
