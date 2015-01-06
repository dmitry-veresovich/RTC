using Rtc.Dal.Dao;

namespace Rtc.Dal.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Rtc.Dal.Dao.EntityModel";
        }

        protected override void Seed(EntityModel context)
        {
            context.Roles.AddOrUpdate(
                role => role.Name,
                new Role { Name = "user", },
                new Role { Name = "admin", }
            );

        }
    }
}