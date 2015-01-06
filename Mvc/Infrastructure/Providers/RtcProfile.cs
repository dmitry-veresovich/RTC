using System.Runtime.CompilerServices;
using System.Web.Profile;
using System.Web.Security;

namespace Rtc.Mvc.Infrastructure.Providers
{
    public class RtcProfile : ProfileBase
    {
        public static RtcProfile GetCurrent()
        {
            var user = Membership.GetUser();
            return user != null ? (RtcProfile)Create(user.UserName) : null;
        }

        public static RtcProfile GetProfile(string email)
        {
            return (RtcProfile)Create(email);
        }

        public string Name
        {
            get { return base[GetCallerName()] as string; }
            set { base[GetCallerName()] = value; }
        }

        public string PhoneNumber
        {
            get { return base[GetCallerName()] as string; }
            set { base[GetCallerName()] = value; }
        }

        public string Email
        {
            get { return base[GetCallerName()] as string; }
            set { base[GetCallerName()] = value; }
        }

        private static string GetCallerName([CallerMemberName] string name = null)
        {
            return name;
        }

    }
}