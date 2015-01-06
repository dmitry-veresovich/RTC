using System.Data.Entity;
using Rtc.Dal.Dao;
using Rtc.Dal.Mappers;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public class UserFriendRepository : BaseRepository<UserFriendDto, UserFriend>, IUserFriendRepository
    {
        public UserFriendRepository(DbContext context) : base(context, new UserFriendMapper()) { }

    }
}