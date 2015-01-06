using System.Collections.Generic;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.VO;

namespace Rtc.BllInterface.Services
{
    public interface IUserFriendService
    {
        IEnumerable<UserEntity> GetFriends(int currentUserId);

        IEnumerable<UserEntity> GetUsersYouFollow(int currentUserId);

        IEnumerable<UserEntity> GetUsersFollowYou(int currentUserId);

        void FollowUser(int currentUserId, int userIdToFollow);

        void UnfollowUser(int currentUserId, int userIdToUnfollow);

        void FriendUser(int currentUserId, int userIdToFriend);

        void UnfriendUser(int currentUserId, int userIdToUnfriend);

        UserRelationsType GetUserRelationsType(int currentUserId, int otherUserId);

    }
}