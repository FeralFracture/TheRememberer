using AutoMapper;
using Microsoft.Extensions.Logging;
using TheRememberer.Infrastructure.Data;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Infrastructure.Repos
{
    public class UserRepo : RepoBase<User, UserDto>, IUserRepo
    {
        public UserRepo(AppDbContext context, IMapper mapper, ILogger<IRepoBase<User, UserDto>> logger) : base(context, mapper, logger)
        {
        }
    }
}
