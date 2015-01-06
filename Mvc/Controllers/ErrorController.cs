using System.Web.Mvc;

namespace Rtc.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BadBrowser()
        {
            return View();
        }
    }
}