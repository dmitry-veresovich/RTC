using Autofac;
using Rtc.Bll.Services;
using Rtc.BllInterface.Services;

namespace Rtc.DependencyResolver
{
    public class BllModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<UsersService>().As<IUsersService>();
            builder.RegisterType<UserFriendService>().As<IUserFriendService>();

            base.Load(builder);
        }
    }
}
