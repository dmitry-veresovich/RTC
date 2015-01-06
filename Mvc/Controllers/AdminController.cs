using System.Web.Mvc;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.Mvc.Hubs;
using Rtc.Mvc.Infrastructure;

namespace Rtc.Mvc.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.SignedUpUsers = RtcDependencyResolver.GetService<IUsersService>().GetAmount();
            ViewBag.OnlineUsers = ChatHub.UsersAmountOnline;
            return View();
        }

        public ActionResult CreateRole(string roleName)
        {
            var roleService = RtcDependencyResolver.GetService<IRoleService>();
            roleService.CreateRole(new RoleEntity { Name = roleName });
            return Content(string.Format("Role {0} succesfully created.", roleName));
        }

    }
}