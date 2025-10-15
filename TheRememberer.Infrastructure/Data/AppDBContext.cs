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
        public DbSet<User_Discord> DiscordUsers => Set<User_Discord>();
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

            modelBuilder.Entity<User>()
                .HasOne(u => u.DiscordData)
                .WithOne(d => d.User)
                .HasForeignKey<User_Discord>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User_Discord>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}