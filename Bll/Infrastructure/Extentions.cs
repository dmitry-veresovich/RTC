using System.Linq;
using Rtc.BllInterface.VO;

namespace Rtc.Bll.Infrastructure
{
    class Extentions
    {
        public static string NormalizePhoneNumber(string phoneNumber)
        {
            return phoneNumber[0] == '+' ? phoneNumber.Remove(0, 1) : phoneNumber;
        }

        public static LogInType GetLogInType(string logInToken)
        {
            return logInToken.Contains('@') ? LogInType.Email : LogInType.PhoneNumber;
        }

    }
}