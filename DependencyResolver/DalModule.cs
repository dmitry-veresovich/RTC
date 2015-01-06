using System.Data.Entity;
using Autofac;
using Rtc.Dal.Dao;
using Rtc.Dal.Repository;
using Rtc.DalInterface.Repository;
using User = Rtc.Dal.Repository.UserRepository;

namespace Rtc.DependencyResolver
{
    public class DalModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();
            builder.RegisterType<UserFriendRepository>().As<IUserFriendRepository>();
            builder.RegisterType<EntityModel>().As<DbContext>().InstancePerRequest();

            base.Load(builder);
        }
    }
}