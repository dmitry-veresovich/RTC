using System.Data.Entity;

namespace Rtc.Dal.Dao
{
    public class EntityModel : DbContext
    {
        public EntityModel()
            : base("RTCDB")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<UserFriend> UserFriends { get; set; }
    }
}
