using Microsoft.EntityFrameworkCore;
using MusicAPI.Models;

namespace MusicAPI.Data
{
    public class ApiDbContext :DbContext
    {

        public ApiDbContext(DbContextOptions<ApiDbContext>options):base(options)
        {
            
        }

        public DbSet<Songs> Songs { get; set; }
        public DbSet<Artists> Artists { get; set; }
        public DbSet<Album> Albums { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        // modelBuilder.Entity<Songs>().HasData(new Songs { });
        //}
    }
}
