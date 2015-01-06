using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Rtc.DalInterface.VO;

namespace Rtc.Dal.Dao
{
    [Table("UserFriend")]
    public class UserFriend
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

        #region Navigation Properties

        public ICollection<User> Users { get; set; } 

        #endregion
    }
}