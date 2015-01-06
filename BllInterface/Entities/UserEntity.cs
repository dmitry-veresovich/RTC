namespace Rtc.BllInterface.Entities
{
    public class UserEntity : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        public int RoleId { get; set; }

    }
}
