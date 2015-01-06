using System.Web.Mvc;

namespace Rtc.Mvc.Infrastructure
{
    public static class RtcDependencyResolver
    {
        public static TService GetService<TService>()
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService<TService>();
        }
    }
}