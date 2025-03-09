using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Extensions;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.IdentityServerData.Connections;
using CinemaTiketsShop.IdentityServerData.Models;
using CinemaTiketsShop.IdentityServerData.Services.Interfaces;
using CinemaTiketsShop.IdentityServerData.Services.Repositories;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.Services.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using static System.Net.WebRequestMethods;

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

            builder.Services.AddDbContext<IdentityServerContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityServer"), builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
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
            builder.Services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("Redis")!)); //Redis configuration
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddIdentity<User, IdentityRole>(Options =>
            {
                Options.Password.RequiredLength = 13;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireUppercase = true;
                Options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
                .AddEntityFrameworkStores<IdentityServerContext>();

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme =
                x.DefaultChallengeScheme =
                x.DefaultScheme =
                x.DefaultSignOutScheme =
                x.DefaultForbidScheme =
                x.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Authority = "";
                x.MetadataAddress = "";
                x.RequireHttpsMetadata = false;
                x.IncludeErrorDetails = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true
                };
            });

            builder.Services.AddAuthorization();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Cinemas}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
