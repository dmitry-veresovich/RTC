using Rtc.Dal.Dao;
using Rtc.DalInterface.Dto;

namespace Rtc.Dal.Mappers
{
    public class UserMapper : IMapper<UserDto, User>
    {
        public UserDto ToDto(User user)
        {
            if (user == null)
                return null;
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo,
                Id = user.Id,
                RoleId = user.RoleId,
            };
        }

        public User ToOrm(UserDto user)
        {
            if (user == null)
                return null;
            return new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo,
                Id = user.Id,
                RoleId = user.RoleId,
            };
        }
    }
}