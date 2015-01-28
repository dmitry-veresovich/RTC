﻿using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.Mvc.Infrastructure;
using Rtc.Mvc.Infrastructure.Providers;
using Rtc.Mvc.Mappers;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        #region Ctor
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        #endregion

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return RedirectToHome();
            if (Membership.ValidateUser(model.LogInToken, model.Password))
            {
                var userName = accountService.GetAccount(model.LogInToken).Email;
                FormsAuthentication.SetAuthCookie(userName, model.RememberMe);
                return RedirectToHome();
            }
            ModelState.AddModelError("", "Wrong email or phone number and / or password.");
            return RedirectToHome();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return RedirectToHome();
            var flag = false;
            if (accountService.AccountExists(model.Email, LogInType.Email))
            {
                ModelState.AddModelError("Email", "There is a user with such email.");
                flag = true;
            }
            if (accountService.AccountExists(model.PhoneNumber, LogInType.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "There is a user with such phone number.");
                flag = true;
            }
            if (model.Photo != null && model.Photo.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("", "Only .jpg photos are allowed.");
                flag = true;
            }
            if (flag)
            {
                return RedirectToHome();
            }

            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            var userRoleName = WebConfigReader.GetRoleName("user");
            var role = roleService.GetRole(userRoleName);
            if (role == null)
            {
                roleService.CreateRole(new RoleEntity { Name = userRoleName });
                role = roleService.GetRole(userRoleName);
            }
 
            

            var userEntity = model.ToEntity(role.Id, PhotoLoader.Load(model.Photo));

            var provider = (RtcMembershipProvider)Membership.Provider;
            if (provider.CreateUser(userEntity, Crypto.HashPassword(model.Password)) != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToHome();
            }
            ModelState.AddModelError("", "Registration error. Try again later.");
            return RedirectToHome();
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToHome();
        }

        private ActionResult RedirectToHome()
        {
            return RedirectToAction("Index", "Home");
        }

        #endregion

    }
}