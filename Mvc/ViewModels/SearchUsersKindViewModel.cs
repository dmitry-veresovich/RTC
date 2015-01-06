using System.ComponentModel.DataAnnotations;

namespace Rtc.Mvc.ViewModels
{
    public enum SearchUsersKindViewModel
    {
        [Display(Name = "Email")]
        Email,
        [Display(Name = "Phone Number")]
        PhoneNumber,
        [Display(Name = "Name")]
        Name,
    }
}