using System.Web.Mvc;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(ErrorViewModel model)
        {
            return View(model);
        }

        public ActionResult BadBrowser()
        {
            return View();
        }
    }
}