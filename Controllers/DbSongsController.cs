using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAPI.Data;
using MusicAPI.Helpers;
using MusicAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbSongsController : ControllerBase
    {
        // GET: api/<DbSongsController>

        private ApiDbContext _dbContext;
        public DbSongsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public async Task<IActionResult>  Get()
        {
            return Ok(await _dbContext.Songs.ToListAsync());    
        }

        // GET api/<DbSongsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           var song=  await( _dbContext.Songs.FindAsync(id));
            if (song == null)
            {
                return NotFound("No record found for id :" + id);
            }
            return Ok(song);
        }


        //// POST api/<DbSongsController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Songs song)
        //{
        //    try
        //    {
        //        await _dbContext.AddAsync(song);
        //        await _dbContext.SaveChangesAsync();

        //        var result = new
        //        {
        //            success = true,
        //            count = await _dbContext.Songs.CountAsync(),
        //            song = song
        //        };

        //        return StatusCode(StatusCodes.Status201Created);
        //        return Ok(result);
        //    }
        //    catch (Exception e)
        //    {
        //        var result = new
        //        {

        //            success = true,
        //            message = e.Message

        //        };
        //    return Ok(result);
        //    }

        //}

        // POST api/<DbSongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Songs song)
        {
            try
            {
                var imageUrl = await FileUpload.ImageUpload(song.Image);
                song.ImageUrl = imageUrl;
                await _dbContext.AddAsync(song);
                await _dbContext.SaveChangesAsync();

                var result = new
                {
                    success = true,
                    count = await _dbContext.Songs.CountAsync(),
                    song = song
                };

                return StatusCode(StatusCodes.Status201Created);
                return Ok(result);
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

        // PUT api/<DbSongsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Songs song)
        {
            try
            {
                var updatedSong = await _dbContext.Songs.FindAsync(id);
                if (updatedSong == null)
                {
                    return NotFound("No record found for id :" + id);
                }
                else
                {
                    updatedSong.title = song.title;
                    updatedSong.artist = song.artist;
                    updatedSong.language = song.language;

                    //_dbContext.Songs.Update(updatedSong);
                   await _dbContext.SaveChangesAsync();

                    var result = new
                    {
                        success = true,
                        count = await _dbContext.Songs.CountAsync(),
                        song = updatedSong
                    };

                    return Ok(result);
                }
               
            }
            catch (Exception e)
            {
                var result = new
                {
                    success = false,
                    message = e.Message
                };
                return Ok( result);
            }
        }

        // DELETE api/<DbSongsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                Songs song = await _dbContext.Songs.FindAsync(id);
                _dbContext.Songs.Remove(song);
                await _dbContext.SaveChangesAsync();

                var result = new
                {
                    success = true,
                    count = await _dbContext.Songs.CountAsync(),
                   
                };

                return Ok(result);
            }
            catch (Exception e)
            {
                //return new
                //{
                //    success = false,
                //    message = e.Message
                //};

                return Ok(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }
    }
}
