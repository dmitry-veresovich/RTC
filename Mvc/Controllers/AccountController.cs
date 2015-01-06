using System.Web;
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
        public ActionResult LogIn()
        {
            return Request.IsAuthenticated ? RedirectToLocal() : View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(LogInViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid) return View(model);
            if (Membership.ValidateUser(model.LogInToken, model.Password))
            {
                var userName = accountService.GetAccount(model.LogInToken).Email;
                FormsAuthentication.SetAuthCookie(userName, model.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Wrong email or phone number and / or password.");
            return View(model);
        }


        [AllowAnonymous]
        public ActionResult SignUp()
        {
            return Request.IsAuthenticated ? RedirectToLocal() : View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
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
            if (flag)
            {
                return View(model);
            }

            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            var role = roleService.GetRole("user");
            if (role == null)
            {
                roleService.CreateRole(new RoleEntity { Name = "user" });
                role = roleService.GetRole("user");
            }
            // TODO: user should be in config file

            var userEntity = model.ToEntity(role.Id, LoadPhoto(model.Photo));

            var provider = (RtcMembershipProvider)Membership.Provider;
            if (provider.CreateUser(userEntity, Crypto.HashPassword(model.Password)) != null)
            {
                FormsAuthentication.SetAuthCookie(model.Email, false);
                return RedirectToLocal();
            }
            ModelState.AddModelError("", "Registration error. Try again later.");
            return View(model);
        }

       

        [HttpGet]
        public ActionResult Manage()
        {
            var profile = Profile as RtcProfile;
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(SignUpViewModel model)
        {
            //
            return View();
        }


        [HttpPost]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        #region Private

        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Url.IsLocalUrl(returnUrl) ? Redirect(returnUrl) : RedirectToLocal();
        }

        private ActionResult RedirectToLocal()
        {
            return RedirectToAction("Index", "Chat");
        }

        private static byte[] LoadPhoto(HttpPostedFileBase file)
        {
            if (file == null) return null;
            var photo = new byte[file.ContentLength];
            file.InputStream.Read(photo, 0, file.ContentLength);
            return photo;
        }


        #endregion

    }
}