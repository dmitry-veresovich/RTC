using Rtc.BllInterface.Entities;
using Rtc.BllInterface.VO;

namespace Rtc.BllInterface.Services
{
    public interface IAccountService
    {
        void CreateAccount(UserEntity user, string hashedPassword);

        void UpdateName(string logInToken, string newName, LogInType logInType);

        void UpdateEmail(string logInToken, string newEmal, LogInType logInType);

        void UpdatePhoneNumber(string logInToken, string phoneNumber, LogInType logInType);

        void UpdatePhoto(string logInToken, byte[] photo, LogInType logInType);


        UserEntity GetAccount(int id);

        UserEntity GetAccount(string logInToken);

        UserEntity GetAccount(string logInToken, LogInType logInType);


        string GetHashedPassword(string logInToken);

        string GetHashedPassword(string logInToken, LogInType logInType);


        void SetHashedPassword(string logInToken, string newHashedPassword);

        void SetHashedPassword(string logInToken, string newHashedPassword, LogInType logInType);


        bool AccountExists(string logInToken);

        bool AccountExists(string logInToken, LogInType logInType);

    }
}