using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


using AlbumApp.Data;
using AlbumApp.Models;
using AlbumApp.Utility;

namespace AlbumApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AlbumContext>(conn => conn.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped<IAlbumRepository, SQLiteRepository>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IComputerVisionService, ComputerVisionService>();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AlbumContext>();
            
            services.AddOptions();
            services.Configure<APIConfig>(Configuration.GetSection("ComputerVision"));
            
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
