using Rtc.BllInterface.Entities;

namespace Rtc.BllInterface.Services
{
    public interface IRoleService
    {
        void CreateRole(RoleEntity role);

        RoleEntity GetRole(string name);

        RoleEntity GetRole(int id);
    }
}