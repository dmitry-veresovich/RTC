using System.Collections.Generic;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.VO;

namespace Rtc.BllInterface.Services
{
    public interface IUsersService
    {
        IEnumerable<UserEntity> Search(int userId, string searchToken, SearchUserKind searchUserKind);

        int GetAmount();
    }
}