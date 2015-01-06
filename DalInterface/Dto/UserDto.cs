namespace Rtc.DalInterface.Dto
{
    public class UserDto : IDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Photo { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public int RoleId { get; set; }
    }
}
