using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.Mvc.Infrastructure.Text;
using Rtc.Mvc.Mappers;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        #region Ctor

        private readonly IUsersService usersService;
        private readonly IAccountService accountService;
        private readonly IUserFriendService userFriendService;


        public UsersController(IUsersService usersService, IAccountService accountService, IUserFriendService userFriendService)
        {
            this.usersService = usersService;
            this.accountService = accountService;
            this.userFriendService = userFriendService;
        }

        #endregion

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(SearchUsersViewModel model)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadBrowser", "Error");
            }

            IEnumerable<SearchUsersResultItemViewModel> searchResults;
            if (string.IsNullOrWhiteSpace(model.SearchToken))
            {
                searchResults = null;
            }
            else
            {
                var currentUser = accountService.GetAccount(Profile.UserName, LogInType.Email).Id;
                searchResults = usersService.Search(currentUser,
                    model.SearchToken, model.SearchUserKind.ToSearchUserKind())
                    .Take(10)
                    .Select(entity => entity.ToViewModel(userFriendService.GetUserRelationsType(currentUser, entity.Id)));
            }
            return PartialView("_SearchResults", searchResults);
        }

        [HttpPost]
        public ActionResult FriendAction(int otherUserId)
        {
            if (!Request.IsAjaxRequest())
            {
                return RedirectToAction("BadBrowser", "Error");
            }

            // TODO: get info about relationsType from client and check whether it's changed

            var currentUserId = accountService.GetAccount(Profile.UserName, LogInType.Email).Id;
            var relationsType = userFriendService.GetUserRelationsType(currentUserId, otherUserId);
            switch (relationsType)
            {
                case UserRelationsType.NotFriends:
                    userFriendService.FollowUser(currentUserId, otherUserId);
                    return new JsonResult { Data = new { text = UserRelationsType.YouFollow.ToText(), id = otherUserId, } };
                case UserRelationsType.Friends:
                    userFriendService.UnfriendUser(currentUserId, otherUserId);
                    return new JsonResult { Data = new { text = UserRelationsType.FollowsYou.ToText(), id = otherUserId, } };
                case UserRelationsType.FollowsYou:
                    userFriendService.FriendUser(currentUserId, otherUserId);
                    return new JsonResult { Data = new { text = UserRelationsType.Friends.ToText(), id = otherUserId, } };
                case UserRelationsType.YouFollow:
                    userFriendService.UnfollowUser(currentUserId, otherUserId);
                    return new JsonResult { Data = new { text = UserRelationsType.NotFriends.ToText(), id = otherUserId, } };
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

    }
}