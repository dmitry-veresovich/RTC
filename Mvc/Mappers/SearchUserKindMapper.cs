using System;
using Rtc.BllInterface.VO;
using Rtc.Mvc.ViewModels;

namespace Rtc.Mvc.Mappers
{
    public static class SearchUserKindMapper
    {
        public static SearchUserKind ToSearchUserKind(this SearchUsersKindViewModel model)
        {
            SearchUserKind searchUserKind;
            if (Enum.TryParse(Enum.GetName(typeof(SearchUsersKindViewModel), model), out searchUserKind))
                return searchUserKind;
            throw new Exception(); // TODO: exc
        }
    }
}