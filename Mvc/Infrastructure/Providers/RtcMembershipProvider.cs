using System;
using System.Web.Helpers;
using System.Web.Security;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;

namespace Rtc.Mvc.Infrastructure.Providers
{
    public class RtcMembershipProvider : MembershipProvider
    {
        public MembershipUser CreateUser(UserEntity user, string password)
        {
            try
            {
                var accountService = RtcDependencyResolver.GetService<IAccountService>();
                if (accountService.AccountExists(user.Email, LogInType.Email) ||
                    accountService.AccountExists(user.PhoneNumber, LogInType.PhoneNumber))
                    return null;
                accountService.CreateAccount(user, password);
                return GetMembershipUser(user);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override bool ValidateUser(string logInToken, string password)
        {
            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var hashedPassword = accountService.GetHashedPassword(logInToken);
            return hashedPassword != null && Crypto.VerifyHashedPassword(hashedPassword, password);
        }

        public override MembershipUser GetUser(string logInToken, bool userIsOnline)
        {
            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var user = accountService.GetAccount(logInToken);
            return user == null ? null : GetMembershipUser(user);
        }

        private static MembershipUser GetMembershipUser(UserEntity user)
        {
            return new MembershipUser("RtcMembershipProvider", user.Email,  null, user.Email, null, null, false, false,
                DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }

        #region NotImplementedException

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer,
           bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.Success;
            return null;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            return false;
        }

        public override string GetPassword(string username, string answer)
        {
            return null;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            return false;
        }

        public override string ResetPassword(string username, string answer)
        {
            return null;
        }

        public override void UpdateUser(MembershipUser user)
        {
        }

        public override bool UnlockUser(string userName)
        {
            return false;
        }



        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override string ApplicationName { get; set; }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}