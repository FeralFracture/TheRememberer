using Microsoft.EntityFrameworkCore;
using TheRememberer.Objects.Entities;

namespace TheRememberer.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Tag> Tags => Set<Tag>();
        public DbSet<ImageTag> ImageTags => Set<ImageTag>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure ImageTag as composite key
            modelBuilder.Entity<ImageTag>()
                .HasKey(it => new { it.ImageId, it.TagId });

            // Configure relationships
            modelBuilder.Entity<ImageTag>()
                .HasOne(it => it.Image)
                .WithMany(i => i.ImageTags)
                .HasForeignKey(it => it.ImageId);

            modelBuilder.Entity<ImageTag>()
                .HasOne(it => it.Tag)
                .WithMany(t => t.ImageTags)
                .HasForeignKey(it => it.TagId);

            // Optional: configure string lengths, indexes, etc.
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}