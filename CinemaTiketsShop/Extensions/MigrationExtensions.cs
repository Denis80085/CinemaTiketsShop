using CinemaTiketsShop.Data;
using CinemaTiketsShop.IdentityServerData.Connections;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.Extensions
{
    public static class MigrationExtensions
    {
        public static void AplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using ApplicationDbConntext context = scope.ServiceProvider.GetRequiredService<ApplicationDbConntext>();

            context.Database.Migrate();

            using IdentityServerContext IdentiyContext = scope.ServiceProvider.GetRequiredService<IdentityServerContext>();

            IdentiyContext.Database.Migrate();
        }
    }
}
