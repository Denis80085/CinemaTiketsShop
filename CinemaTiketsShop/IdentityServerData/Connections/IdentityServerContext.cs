using CinemaTiketsShop.IdentityServerData.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CinemaTiketsShop.IdentityServerData.Connections
{
    public class IdentityServerContext : IdentityDbContext<User>
    {
        public IdentityServerContext(DbContextOptions<IdentityServerContext> options) : base(options)
        {
        }
        
    }
}
