using Rtc.BllInterface.Entities;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Mappers
{
    public static class SignUpMapper
    {
        public static UserEntity ToEntity(this SignUpViewModel model, int roleId, byte[] photo)
        {
            if (model == null)
                return null;
            return new UserEntity
            {
                Email = model.Email,
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Photo = photo,
                RoleId = roleId,  
            };
        }

    }
}