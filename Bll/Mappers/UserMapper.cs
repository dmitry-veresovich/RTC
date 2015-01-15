using Rtc.BllInterface.Entities;
using Rtc.DalInterface.Dto;

namespace Rtc.Bll.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this UserEntity user, string password = null)
        {
            if (user == null)
                return null;
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo,
                Password = password,
                RoleId = user.RoleId,
            };
        }

        public static UserEntity ToUserEntity(this UserDto user)
        {
            if (user == null)
                return null;
            return new UserEntity
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Photo = user.Photo,
                RoleId = user.RoleId,
            };
        }
    }
}
