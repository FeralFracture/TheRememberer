using AutoMapper;
using Microsoft.Extensions.Logging;
using TheRememberer.Infrastructure.Data;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Infrastructure.Repos
{
    public class User_DiscordRepo : RepoBase<User_Discord, User_DiscordDto>, IUser_DiscordRepo
    {
        public User_DiscordRepo(AppDbContext context, IMapper mapper, ILogger<IRepoBase<User_Discord, User_DiscordDto>> logger) : base(context, mapper, logger)
        {
        }
    }
}
