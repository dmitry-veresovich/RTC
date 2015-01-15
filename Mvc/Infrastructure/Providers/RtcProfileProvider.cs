using System;
using System.Configuration;
using System.Web.Profile;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;

namespace Rtc.Mvc.Infrastructure.Providers
{
    public class RtcProfileProvider : ProfileProvider
    {
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context,
            SettingsPropertyCollection collection)
        {
            var result = new SettingsPropertyValueCollection();
            if (collection == null || collection.Count < 1 || context == null)
                return result;

            var email = (string)context["UserName"];
            if (String.IsNullOrEmpty(email))
                return result;

            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var user = accountService.GetAccount(email, LogInType.Email);
            if (user == null)
                return result;

            foreach (SettingsProperty prop in collection)
            {
                var spv = new SettingsPropertyValue(prop)
                {
                    PropertyValue = user.GetType().GetProperty(prop.Name).GetValue(user),
                };
                result.Add(spv);
            }
            return result;
        }

        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            var email = (string)context["UserName"];
            if (string.IsNullOrEmpty(email) || collection.Count < 1)
                return;
            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var user = accountService.GetAccount(email, LogInType.Email);
            if (user == null)
                return;

            foreach (SettingsPropertyValue val in collection)
            {
                user.GetType().GetProperty(val.Property.Name).SetValue(user, val.PropertyValue);
            }
        }


        #region NotImplementedException

        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}