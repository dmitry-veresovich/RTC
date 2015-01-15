using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Rtc.Mvc.ViewModels
{
    public class ManageViewModel
    {
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email address")]
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Wrong email address format.")]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone number")]
        [Phone(ErrorMessage = "Wrong phone number format.")]
        public string PhoneNumber { get; set; }


        [Display(Name = "Current password")]
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Display(Name = "Password")]
        [Required]
        [StringLength(maximumLength: 16, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirmation password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public HttpPostedFileBase Photo { get; set; }

    }
}