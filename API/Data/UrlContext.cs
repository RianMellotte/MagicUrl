using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UrlContext : DbContext
    {
        public UrlContext(DbContextOptions options) : base(options)
        {
            this.Database.Migrate();
        }

        public DbSet<UrlEntity> UrlRepo { get; set; }
    }
}