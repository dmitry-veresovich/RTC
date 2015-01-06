using System.Web.Mvc;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.Mvc.Mappers;

namespace Rtc.Mvc.Controllers
{
    [Authorize]
    public class ChatController : Controller
    {
        #region Ctor

        private readonly IAccountService accountService;
        private readonly IUserFriendService userFriendService;

        public ChatController(IAccountService accountService, IUserFriendService userFriendService)
        {
            this.accountService = accountService;
            this.userFriendService = userFriendService;
        }

        #endregion


        // GET: Chat
        public ActionResult Index()
        {
            SetUpFriendsPartial(accountService.GetAccount(Profile.UserName, LogInType.Email).Id);
            return View();
        }

        [HttpPost]
        public ActionResult ChattingTo(int id)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadBrowser", "Error");
            }

            // TODO: chat

            var user = accountService.GetAccount(Profile.UserName, LogInType.Email);
            ViewBag.User = user;
            ViewBag.OtherUser = accountService.GetAccount(id).ToViewModel(userFriendService.GetUserRelationsType(user.Id, id));
            return PartialView("_ChattingTo");
        }



        #region Private

        private void SetUpFriendsPartial(int id)
        {
            ViewBag.Friends = userFriendService.GetFriends(id);
            ViewBag.FollowYou = userFriendService.GetUsersFollowYou(id);
            ViewBag.YouFollow = userFriendService.GetUsersYouFollow(id);
        }

        #endregion


    }
}