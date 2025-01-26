using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Services;
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
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped<IActorServices, ActorService>();
            builder.Services.AddScoped<IProducerService, ProducerService>();
            builder.Services.AddScoped<IPhotoService, PhotoService>();
            builder.Services.AddScoped<IPictureUploader, PictureUploader>();

            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("AccountSettings"));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
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
