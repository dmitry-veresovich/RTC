using System;
using System.Web.Security;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;

namespace Rtc.Mvc.Infrastructure.Providers
{
    public class RtcRoleProvider : RoleProvider
    {
        public override bool IsUserInRole(string username, string roleName)
        {
            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var user = accountService.GetAccount(username);
            if (user == null)
                return false;
            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            var role = roleService.GetRole(user.Id);
            return role != null && role.Name == roleName;
        }

        public override string[] GetRolesForUser(string logInToken)
        {
            var accountService = RtcDependencyResolver.GetService<IAccountService>();
            var user = accountService.GetAccount(logInToken);
            if (user == null)
                return null;
            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            var role = roleService.GetRole(user.RoleId);
            return role != null ? new[] { role.Name } : null;
        }

        public override void CreateRole(string roleName)
        {
            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            roleService.CreateRole(new RoleEntity { Name = roleName, });
        }

        #region NotImplementedException

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        #endregion
    }
}