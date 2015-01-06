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
                        relation = "Unfriend";
                        break;
                    }
                case UserRelationsType.FollowsYou:
                    relation = "Friend";
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