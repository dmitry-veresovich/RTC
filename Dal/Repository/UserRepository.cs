using System;
using System.Data.Entity;
using System.Linq.Expressions;
using Rtc.Dal.Dao;
using Rtc.Dal.Mappers;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;

namespace Rtc.Dal.Repository
{
    public class UserRepository : BaseRepository<UserDto, User>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context, new UserMapper()) { }

        private void UpdateProperty<TProperty>(Expression<Func<User, TProperty>> property, UserDto user)
        {
            var userOrm = mapper.ToOrm(user);
            context.Set<User>().Attach(userOrm);
            context.Entry(userOrm).Property(property).IsModified = true;
        }


        public void UpdatePassword(UserDto user)
        {
            UpdateProperty(u => u.Password, user);
        }

        public void UpdateName(UserDto user)
        {
            UpdateProperty(u => u.Name, user);
        }

        public void UpdateEmail(UserDto user)
        {
            UpdateProperty(u => u.Email, user);
        }

        public void UpdatePhoneNumber(UserDto user)
        {
            UpdateProperty(u => u.PhoneNumber, user);
        }

        public void UpdatePhoto(UserDto user)
        {
            UpdateProperty(u => u.Photo, user);
        }
    }
}