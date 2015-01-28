using System.Collections.Generic;
using System.Linq;
using System.Web.Helpers;
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
        public JsonResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(model.LogInToken, model.Password))
                {
                    var userName = accountService.GetAccount(model.LogInToken).Email;
                    FormsAuthentication.SetAuthCookie(userName, model.RememberMe);
                    return Json(new { success = true, redirect = returnUrl });
                }
                ModelState.AddModelError("", "Wrong email or phone number and / or password.");
            }
            return Json(new { errors = GetErrorsFromModelState() });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SignUp(SignUpViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
                return Json(new { errors = GetErrorsFromModelState() });
            if (accountService.AccountExists(model.Email, LogInType.Email))
            {
                ModelState.AddModelError("", "There is a user with such email.");
                return Json(new { errors = GetErrorsFromModelState() });
            }
            if (accountService.AccountExists(model.PhoneNumber, LogInType.PhoneNumber))
            {
                ModelState.AddModelError("", "There is a user with such phone number.");
                return Json(new { errors = GetErrorsFromModelState() });
            }
            if (model.Photo != null && model.Photo.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("", "Only .jpg photos are allowed.");
                return Json(new { errors = GetErrorsFromModelState() });
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
                return Json(new { success = true, redirect = returnUrl });
            }
            ModelState.AddModelError("", "Registration error. Try again later.");
            return Json(new { errors = GetErrorsFromModelState() });
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Helpers

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #endregion

    }
}