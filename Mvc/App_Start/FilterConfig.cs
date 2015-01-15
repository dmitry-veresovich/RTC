using System.Web.Mvc;
using Rtc.Mvc.Filters;

namespace Rtc.Mvc
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new ExceptionFilterAttribute());
        }
    }
}