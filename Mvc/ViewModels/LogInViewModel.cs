using System.ComponentModel.DataAnnotations;
using Rtc.Mvc.Infrastructure.Validation;

namespace Rtc.Mvc.ViewModels
{
    public class LogInViewModel
    {
        [Display(Name = "Email adress or phone number")]
        [Required]
        [LogInToken(ErrorMessage = "Neither email adress nor phone number are valid.")]
        public string LogInToken { get; set; }

        [Display(Name = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

    }
}