using System.ComponentModel.DataAnnotations;

namespace Rtc.Mvc.Infrastructure.Validation
{
    public class LogInTokenAttribute : ValidationAttribute
    {
        private readonly EmailAddressAttribute emailAddress = new EmailAddressAttribute();
        private readonly PhoneAttribute phone = new PhoneAttribute();

        public override bool IsValid(object value)
        {
            return emailAddress.IsValid(value) || phone.IsValid(value);
        }
    }
}