using AutoMapper;
using Microsoft.Extensions.Logging;
using TheRememberer.Objects;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;

namespace TheRemember.Tests.Objects
{

    public class AutoMapperProfileTests
    {
        private readonly IMapper _mapper;

        public AutoMapperProfileTests()
        {
            // This runs before any test in this class
            ILoggerFactory loggerFactory = LoggerFactory.Create(builder => { });
            var profile = new AutoMapperProfile();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile);
            }, loggerFactory);

            // Assert config validity once
            config.AssertConfigurationIsValid();

            // Store mapper for all tests
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void AutoMapper_Config_Is_Valid()
        {
            // Already asserted in constructor, but you can still keep this test
            Assert.NotNull(_mapper);
        }

        [Fact]
        public void DISCORD_Dto_to_Entity_Is_Valid()
        {
            var dto = new User_DiscordDto
            {
                AccessToken = "DAccessToken",
                AvatarHash = "A_Hash",
                DiscordId = 123456,
                CreatedAt = DateTime.Now,
                DisplayName = "DisplayName",
                DbId = Guid.NewGuid(),
                RefreshToken = "DRefreshToken",
                TokenExpiration = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserName = "Username"
            };
            var entity = _mapper.Map<User_Discord>(dto);
            Assert.Equal(entity.AccessToken, dto.AccessToken);
            Assert.Equal(entity.AvatarHash, dto.AvatarHash);
            Assert.Equal(entity.DiscordId, dto.DiscordId);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
            Assert.Equal(entity.DisplayName, dto.DisplayName);
            Assert.Equal(entity.Id, dto.DbId);
            Assert.Equal(entity.RefreshToken, dto.RefreshToken);
            Assert.Equal(entity.TokenExpiration, dto.TokenExpiration);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
            Assert.Equal(entity.UserId, dto.UserId);
            Assert.Equal(entity.UserName, dto.UserName);
        }
        [Fact]
        public void DISCORD_Entity_to_Dto_Is_Valid()
        {
            var entity = new User_Discord
            {
                AccessToken = "DAccessToken",
                AvatarHash = "A_Hash",
                DiscordId = 123456,
                CreatedAt = DateTime.Now,
                DisplayName = "DisplayName",
                Id = Guid.NewGuid(),
                RefreshToken = "DRefreshToken",
                TokenExpiration = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = Guid.NewGuid(),
                UserName = "Username"
            };
            var dto = _mapper.Map<User_DiscordDto>(entity);
            Assert.Equal(entity.AccessToken, dto.AccessToken);
            Assert.Equal(entity.AvatarHash, dto.AvatarHash);
            Assert.Equal(entity.DiscordId, dto.DiscordId);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
            Assert.Equal(entity.DisplayName, dto.DisplayName);
            Assert.Equal(entity.Id, dto.DbId);
            Assert.Equal(entity.RefreshToken, dto.RefreshToken);
            Assert.Equal(entity.TokenExpiration, dto.TokenExpiration);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
            Assert.Equal(entity.UserId, dto.UserId);
            Assert.Equal(entity.UserName, dto.UserName);
        }

        [Fact]
        public void USER_Dto_to_Entity_Is_Valid()
        {
            var user_id = Guid.NewGuid();
            var discordDataDto = new User_DiscordDto
            {
                AccessToken = "DAccessToken",
                AvatarHash = "A_Hash",
                DiscordId = 123456,
                CreatedAt = DateTime.Now,
                DisplayName = "DisplayName",
                DbId = Guid.NewGuid(),
                RefreshToken = "DRefreshToken",
                TokenExpiration = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = user_id,
                UserName = "Username"
            };
            var dto = new UserDto
            {
                AccessToken = "UAccessToken",
                CreatedAt = DateTime.Now,
                DbId = user_id,
                RefreshToken = "URefreshToken",
                TokenExpiration = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DiscordData = discordDataDto
            };
            var entity = _mapper.Map<User>(dto);
            Assert.Equal(entity.DiscordData!.DisplayName, dto.DiscordData!.DisplayName);
            Assert.Equal(entity.DiscordData.UserName, dto.DiscordData.UserName);
            Assert.Equal(entity.DiscordData.AccessToken, dto.DiscordData.AccessToken);
            Assert.Equal(entity.DiscordData.RefreshToken, dto.DiscordData.RefreshToken);
            Assert.Equal(entity.DiscordData.Id, dto.DiscordData.DbId);
            Assert.Equal(entity.DiscordData.DiscordId, dto.DiscordData.DiscordId);
            Assert.Equal(entity.DiscordData.AvatarHash, dto.DiscordData.AvatarHash);
            Assert.Equal(entity.DiscordData.CreatedAt, dto.DiscordData.CreatedAt);
            Assert.Equal(entity.DiscordData.TokenExpiration, dto.DiscordData.TokenExpiration);
            Assert.Equal(entity.DiscordData.UpdatedAt, dto.DiscordData.UpdatedAt);
            Assert.Equal(entity.DiscordData.UserId, dto.DiscordData.UserId);
            Assert.Equal(entity.Id, dto.DiscordData.UserId);

            Assert.Equal(entity.AccessToken, dto.AccessToken);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
            Assert.Equal(entity.Id, dto.DbId);
            Assert.Equal(entity.RefreshToken, dto.RefreshToken);
            Assert.Equal(entity.TokenExpiration, dto.TokenExpiration);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
        }
        [Fact]
        public void USER_Entity_to_Dto_Is_Valid()
        {
            var user_id = Guid.NewGuid();
            var discordData = new User_Discord
            {
                AccessToken = "DAccessToken",
                AvatarHash = "A_Hash",
                DiscordId = 123456,
                CreatedAt = DateTime.Now,
                DisplayName = "DisplayName",
                Id = Guid.NewGuid(),
                RefreshToken = "DRefreshToken",
                TokenExpiration= DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserId = user_id,
                UserName = "Username"
            };
            var entity = new User
            {
                AccessToken = "UAccessToken",
                CreatedAt = DateTime.Now,
                Id = user_id,
                RefreshToken = "URefreshToken",
                TokenExpiration = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DiscordData = discordData
            };
            discordData.User = entity;
            var dto = _mapper.Map<UserDto>(entity);
            Assert.Equal(entity.DiscordData.DisplayName, dto.DiscordData!.DisplayName);
            Assert.Equal(entity.DiscordData.UserName, dto.DiscordData.UserName);
            Assert.Equal(entity.DiscordData.AccessToken, dto.DiscordData.AccessToken);
            Assert.Equal(entity.DiscordData.RefreshToken, dto.DiscordData.RefreshToken);
            Assert.Equal(entity.DiscordData.Id, dto.DiscordData.DbId);
            Assert.Equal(entity.DiscordData.DiscordId, dto.DiscordData.DiscordId);
            Assert.Equal(entity.DiscordData.AvatarHash, dto.DiscordData.AvatarHash);
            Assert.Equal(entity.DiscordData.CreatedAt, dto.DiscordData.CreatedAt);
            Assert.Equal(entity.DiscordData.TokenExpiration, dto.DiscordData.TokenExpiration);
            Assert.Equal(entity.DiscordData.UpdatedAt, dto.DiscordData.UpdatedAt);
            Assert.Equal(entity.DiscordData.UserId, dto.DiscordData.UserId);
            Assert.Equal(entity.Id, dto.DiscordData.UserId);

            Assert.Equal(entity.AccessToken, dto.AccessToken);
            Assert.Equal(entity.CreatedAt, dto.CreatedAt);
            Assert.Equal(entity.Id, dto.DbId);
            Assert.Equal(entity.RefreshToken, dto.RefreshToken);
            Assert.Equal(entity.TokenExpiration, dto.TokenExpiration);
            Assert.Equal(entity.UpdatedAt, dto.UpdatedAt);
        }
    }
}
