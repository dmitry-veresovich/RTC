using System.Web.Mvc;

namespace Rtc.Mvc.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Index", "Chat");
            }
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

    }
}