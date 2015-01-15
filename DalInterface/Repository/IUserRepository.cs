using Rtc.DalInterface.Dto;

namespace Rtc.DalInterface.Repository
{
    public interface IUserRepository : IRepository<UserDto>
    {
        void UpdatePassword(UserDto user);

        void UpdateName(UserDto user);

        void UpdateEmail(UserDto user);

        void UpdatePhoneNumber(UserDto user);

        void UpdatePhoto(UserDto user);
    }
}