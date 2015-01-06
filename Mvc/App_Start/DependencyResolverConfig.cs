using Autofac;
using Autofac.Integration.Mvc;
using Rtc.DependencyResolver;

namespace Rtc.Mvc
{
    public static class DependencyResolverConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            builder.RegisterModule(new DalModule());
            builder.RegisterModule(new BllModule());

            var containter = builder.Build();
            var autofacDependencyResolver = new AutofacDependencyResolver(containter);
            System.Web.Mvc.DependencyResolver.SetResolver(autofacDependencyResolver);
        }
    }
}