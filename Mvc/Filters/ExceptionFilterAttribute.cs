using System.Web;
using System.Web.Mvc;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Filters
{
    public class ExceptionFilterAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
                return;
            if (exceptionContext.Exception == null || exceptionContext.ExceptionHandled) return;

            var model = new ErrorViewModel();
            var helper = new UrlHelper(exceptionContext.RequestContext);
            var controller = (string)exceptionContext.RequestContext.RouteData.Values["controller"];
            var action = (string)exceptionContext.RequestContext.RouteData.Values["action"];
            model.ReturnUrl = helper.Action(action, controller);
            var url = helper.Action("Index", "Error", model);
            exceptionContext.Result = new RedirectResult(url);
            exceptionContext.ExceptionHandled = true;
        }
    }
}