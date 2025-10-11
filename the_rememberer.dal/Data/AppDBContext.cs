using Microsoft.EntityFrameworkCore;
using TheRememberer.Objects.Entities;

namespace TheRememberer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Image> Images { get; set; } = null!;
    }
}

