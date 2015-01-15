using Rtc.BllInterface.Entities;
using Rtc.BllInterface.VO;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Mappers
{
    public static class UserMapper
    {
        public static UserViewModel ToViewModel(this UserEntity user, UserRelationsType userRelationsType)
        {
            return new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Photo = user.Photo,
                UserRelationsType = userRelationsType,
            };
        }


    }
}