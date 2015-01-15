using System;
using Rtc.BllInterface.VO;

namespace Rtc.Mvc.Infrastructure.Text
{
    public static class TextHelper
    {
        public static string ToText(this UserRelationsType userRelationsType)
        {
            string relation;
            switch (userRelationsType)
            {
                case UserRelationsType.Friends:
                    {
                        relation = "Break up";
                        break;
                    }
                case UserRelationsType.FollowsYou:
                    relation = "Confirm as friend";
                    break;
                case UserRelationsType.YouFollow:
                    relation = "Unfollow";
                    break;
                case UserRelationsType.NotFriends:
                    relation = "Follow";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return relation;
        }


    }
}