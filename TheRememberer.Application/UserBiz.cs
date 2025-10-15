using TheRememberer.Infrastructure.Repos;
using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Application
{
    public class UserBiz : BizBase<User, UserDto, IUserRepo>, IUserBiz
    {
        public UserBiz(IUserRepo repo) : base(repo)
        {
        }
    }
}
