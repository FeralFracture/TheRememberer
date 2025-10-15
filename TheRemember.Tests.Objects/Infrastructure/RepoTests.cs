using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using TheRememberer.Infrastructure.Data;
using TheRememberer.Objects.Entities;

namespace TheRemember.Tests.Infrastructure
{
    public class RepoTests
    {
        private AppDbContext GetInMemoryDb()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new AppDbContext(options);
        }

        [Fact]
        public void USER_Add_Get_Del()
        {
            var context = GetInMemoryDb();
            var item = new User { Id = Guid.NewGuid(), AccessToken = "abc" };
            context.Users.Add(item);
            context.SaveChanges();
            var savedItem = context.Users.FirstOrDefault(u => u.Id == item.Id);
            Assert.NotNull(savedItem);
            Assert.Equal("abc", savedItem.AccessToken);
            context.Users.Remove(item);
            context.SaveChanges();
            Assert.Null(context.Users.FirstOrDefault(u => u.Id == item.Id));
        }
        [Fact]
        public void USER_DISCORD_Add_Get_Del()
        {
            var context = GetInMemoryDb();
            var item = new User_Discord { Id = Guid.NewGuid(), AccessToken = "abc" };
            context.DiscordUsers.Add(item);
            context.SaveChanges();
            var savedItem = context.DiscordUsers.FirstOrDefault(u => u.Id == item.Id);
            Assert.NotNull(savedItem);
            Assert.Equal("abc", savedItem.AccessToken);
            context.DiscordUsers.Remove(item);
            context.SaveChanges();
            Assert.Null(context.DiscordUsers.FirstOrDefault(u => u.Id == item.Id));
        }
        [Fact]
        public void TAG_Add_Get_Del()
        {
            var context = GetInMemoryDb();
            var item = new Tag { Id = Guid.NewGuid(), Value = "abc" };
            context.Tags.Add(item);
            context.SaveChanges();
            var savedItem = context.Tags.FirstOrDefault(u => u.Id == item.Id);
            Assert.NotNull(savedItem);
            Assert.Equal("abc", savedItem.Value);
            context.Tags.Remove(item);
            context.SaveChanges();
            Assert.Null(context.Tags.FirstOrDefault(u => u.Id == item.Id));
        }
        [Fact]
        public void IMAGE_Add_Get_Del()
        {
            var context = GetInMemoryDb();
            var item = new Image { Id = Guid.NewGuid(), FileName = "abc" };
            context.Images.Add(item);
            context.SaveChanges();
            var savedItem = context.Images.FirstOrDefault(u => u.Id == item.Id);
            Assert.NotNull(savedItem);
            Assert.Equal("abc", savedItem.FileName);
            context.Images.Remove(item);
            context.SaveChanges();
            Assert.Null(context.Images.FirstOrDefault(u => u.Id == item.Id));
        }
        [Fact]
        public void IMAGETAG_Add_Get_Del()
        {
            var tagId = Guid.NewGuid();
            var context = GetInMemoryDb();
            var item = new ImageTag { ImageId = Guid.NewGuid(), TagId = tagId };
            context.ImageTags.Add(item);
            context.SaveChanges();
            var savedItem = context.ImageTags.FirstOrDefault(u => u.ImageId == item.ImageId);
            Assert.NotNull(savedItem);
            Assert.Equal(tagId, savedItem.TagId);
            context.ImageTags.Remove(item);
            context.SaveChanges();
            Assert.Null(context.ImageTags.FirstOrDefault(u => u.ImageId == item.ImageId));
        }
    }
}
