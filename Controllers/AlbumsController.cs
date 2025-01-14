﻿using Microsoft.AspNetCore.Http;
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
    public class AlbumsController : ControllerBase
    {
        private ApiDbContext _dbContext;
        public AlbumsController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // POST api/<DbSongsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Album album)
        {
            try
            {
                var imageUrl = await CloudinaryService.CloudinaryImageUpload(album.Image);

                album.ImageUrl = imageUrl;
                
                await _dbContext.AddAsync(album);
                await _dbContext.SaveChangesAsync();

                var result = new
                {
                    success = true,
                    message = "New Album Added Successfully",
                    count = await _dbContext.Songs.CountAsync(),
                    album
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
        public async Task<IActionResult> GetAlbums(int? pageSize, int? pageNumber)
        {
            int currentPageNumber = pageNumber ?? 1;
            int currentPageSize = pageSize ?? 10;
            var albums = await (from album in _dbContext.Albums
                                 select new
                                 {
                                     Id = album.Id,
                                     Nama = album.Name,
                                     ImageUrl = album.ImageUrl,
                                 }).ToListAsync();

            return Ok(albums.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> AlbumDetails(int albumId)
        {

            var albumDetails = await _dbContext.Albums.Where(a => a.Id == albumId).Include(a => a.Songs).ToListAsync();
            return Ok(albumDetails);
        }



    }
}
