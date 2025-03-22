using CinemaTiketsShop.Configs;
using CinemaTiketsShop.Data;
using CinemaTiketsShop.Data.Services;
using CinemaTiketsShop.Extensions;
using CinemaTiketsShop.Helpers;
using CinemaTiketsShop.Helpers.EncryptionHelpers.AES;
using CinemaTiketsShop.Services;
using CinemaTiketsShop.Services.CognitoUserMenager;
using CinemaTiketsShop.Services.CookieService;
using CinemaTiketsShop.Services.Redis;
using CinemaTiketsShop.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Configuration;
using System.Text;

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

            builder.Services.AddScoped<ICookieRepository, CookieRepository>();
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
            builder.Services.Configure<AES_Settings>(builder.Configuration.GetSection("AES_VALS"));
            builder.Services.AddSingleton<IAES_EcryptionHelper, AES_EcryptionHelper>();

            builder.Services.Configure<CognitoAppConfig>(builder.Configuration.GetSection("AppConfig"));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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
                    x.RequireHttpsMetadata = true;
                    x.IncludeErrorDetails = true;
                    x.Authority = builder.Configuration["Cognito:Authority"];
                    x.MetadataAddress = builder.Configuration["Cognito:MetaDataAddress"]!;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true
                    };

                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context => 
                        {
                            context.Request.Cookies.TryGetValue("access_token", out var access_token);

                            if (!string.IsNullOrEmpty(access_token))
                            {
                                context.Token = access_token;
                            }

                            return Task.CompletedTask;
                        }
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
