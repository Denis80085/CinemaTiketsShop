using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Extensions;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.Services.Redis;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbConntext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
            });

            builder.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = builder.Configuration.GetConnectionString("Redis");
            });

            builder.Services.AddScoped<IActorServices, ActorService>();
            builder.Services.AddScoped<IProducerService, ProducerService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IPictureUploader, PictureUploader>();
            builder.Services.AddScoped<ICinemaService, CinemaService>();
            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IActor_MovieService, Actor_MovieService>();
            builder.Services.AddScoped<IRedisCachingService, RedisCachingService>();

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("AccountSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.AplyMigrations();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Movies}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
