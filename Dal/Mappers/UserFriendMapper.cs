using Rtc.Dal.Dao;
using Rtc.DalInterface.Dto;

namespace Rtc.Dal.Mappers
{
    public class UserFriendMapper : IMapper<UserFriendDto, UserFriend>
    {
        public UserFriendDto ToDto(UserFriend userFriend)
        {
            if (userFriend == null)
                return null;
            return new UserFriendDto
            {
                Id = userFriend.Id,
                UserId = userFriend.UserId,
                FriendId = userFriend.FriendId,
                FriendshipStatus = userFriend.FriendshipStatus,
            };
        }
        public UserFriend ToOrm(UserFriendDto userFriend)
        {
            if (userFriend == null)
                return null;
            return new UserFriend
            {
                Id = userFriend.Id,
                UserId = userFriend.UserId,
                FriendId = userFriend.FriendId,
                FriendshipStatus = userFriend.FriendshipStatus,
            };
        }
    }
}