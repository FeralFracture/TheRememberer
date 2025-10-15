using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces;

namespace TheRememberer.Application
{
    public class User_DiscordBiz : BizBase<User_Discord, User_DiscordDto, IUser_DiscordRepo>, IUser_DiscordBiz
    {
        public User_DiscordBiz(IUser_DiscordRepo repo) : base(repo)
        {
        }
    }
}

