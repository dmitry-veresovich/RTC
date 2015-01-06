using System.ComponentModel.DataAnnotations.Schema;

namespace Rtc.Dal.Dao
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int RoleId { get; set; }

        #region Navigation Properties

        public virtual Role Role { get; set; }

        #endregion
    }
}