using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MusicAPI.Data;


namespace MusicAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicAPI", Version = "v1" });
            });
            //connectionString
            services.AddDbContext<ApiDbContext>(options=>options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MusicDb"));


           

           

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext)
        //    {
        //        if (env.IsDevelopment())
        //        {
        //            app.UseDeveloperExceptionPage();
        //            app.UseSwagger();
        //            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicAPI v1"));
        //        }


        //        dbContext.Database.EnsureCreated();
        //        app.UseHttpsRedirection();

        //        app.UseRouting();

        //        app.UseAuthorization();

        //        app.UseEndpoints(endpoints =>
        //        {
        //            endpoints.MapControllers();
        //        });
        //    }
        //}
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MusicAPI v1"));
            }


            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
