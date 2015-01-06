using System.Data.Entity;
using Rtc.Dal.Dao;
using Rtc.Dal.Mappers;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public class UserRepository : BaseRepository<UserDto, User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context, new UserMapper()) { }


    }
}