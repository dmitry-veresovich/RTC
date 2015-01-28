using System.Web.Mvc;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.Mvc.Infrastructure;
using Rtc.Mvc.Mappers;

namespace Rtc.Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                var accountService = RtcDependencyResolver.GetService<IAccountService>();
                var userFriendService = RtcDependencyResolver.GetService<IUserFriendService>();
                
                var id = accountService.GetAccount(Profile.UserName, LogInType.Email).Id;
                ViewBag.Friends = userFriendService.GetFriends(id).ToViewModel();
                ViewBag.FollowYou = userFriendService.GetUsersFollowYou(id).ToViewModel();
                ViewBag.YouFollow = userFriendService.GetUsersYouFollow(id).ToViewModel();
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}