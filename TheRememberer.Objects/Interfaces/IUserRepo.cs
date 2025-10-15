using TheRememberer.Objects.DTOs;
using TheRememberer.Objects.Entities;
using TheRememberer.Objects.Interfaces.Bases;

namespace TheRememberer.Objects.Interfaces
{
    public interface IUserRepo : IRepoBase<User, UserDto>
    {
    }
}
