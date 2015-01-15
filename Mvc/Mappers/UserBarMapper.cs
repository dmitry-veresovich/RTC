using System.Collections.Generic;
using System.Linq;
using Rtc.BllInterface.Entities;
using Rtc.Mvc.Hubs;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Mappers
{
    public static class UserBarMapper
    {
        public static IEnumerable<UserBarViewModel> ToViewModel(this IEnumerable<UserEntity> users)
        {
            return users.Select(user => new UserBarViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Photo = user.Photo,
                IsOnline = ChatHub.IsUserOnline(user.Id),
            });
        }
    }
}