using Rtc.BllInterface.VO;

namespace Rtc.Mvc.ViewModels
{
    public class SearchUsersResultItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public byte[] Photo { get; set; }

        public UserRelationsType UserRelationsType { get; set; }

    }
}