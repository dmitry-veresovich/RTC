using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rtc.Dal.Dao
{
    [Table("Role")]
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        #region Navigation Properties

        public virtual ICollection<User> Users { get; set; }

        #endregion
    }
}