using System.Collections.Generic;
using System.Linq;
using Rtc.Bll.Mappers;
using Rtc.BllInterface.Entities;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.DalInterface.Dto;
using Rtc.DalInterface.Repository;
using Rtc.DalInterface.VO;

namespace Rtc.Bll.Services
{
    public class UserFriendService : IUserFriendService
    {
        #region Ctor

        private readonly IUnitOfWork uow;
        private readonly IUserFriendRepository userFriendRepository;
        private readonly IUserRepository userRepository;

        public UserFriendService(IUnitOfWork uow, IUserFriendRepository userFriendRepository, IUserRepository userRepository)
        {
            this.uow = uow;
            this.userFriendRepository = userFriendRepository;
            this.userRepository = userRepository;
            //Debug.WriteLine("service create!");
        }

        #endregion


        public IEnumerable<UserEntity> GetFriends(int currentUserId)
        {
            return GetUsers(currentUserId, FriendshipStatus.Friends);
        }

        public IEnumerable<UserEntity> GetUsersYouFollow(int currentUserId)
        {
            return GetUsers(currentUserId, FriendshipStatus.YouFollow);
        }

        public IEnumerable<UserEntity> GetUsersFollowYou(int currentUserId)
        {
            return GetUsers(currentUserId, FriendshipStatus.FollowsYou);
        }


        public void FollowUser(int currentUserId, int userIdToFollow)
        {
            var userFriendDto = new UserFriendDto
            {
                UserId = currentUserId,
                FriendId = userIdToFollow,
                FriendshipStatus = FriendshipStatus.YouFollow,
            };
            userFriendRepository.Create(userFriendDto);

            userFriendDto = new UserFriendDto
            {
                UserId = userIdToFollow,
                FriendId = currentUserId,
                FriendshipStatus = FriendshipStatus.FollowsYou,
            };
            userFriendRepository.Create(userFriendDto);

            uow.Commit();
        }

        public void UnfollowUser(int currentUserId, int userIdToUnfollow)
        {
            userFriendRepository.Delete(GetUserFriendDto(currentUserId, userIdToUnfollow).Id);
            userFriendRepository.Delete(GetUserFriendDto(userIdToUnfollow, currentUserId).Id);
            uow.Commit();
        }

        public void FriendUser(int currentUserId, int userIdToFriend)
        {
            var userFriendDto = GetUserFriendDto(currentUserId, userIdToFriend);
            userFriendDto.FriendshipStatus = FriendshipStatus.Friends;
            userFriendRepository.Update(userFriendDto);

            userFriendDto = GetUserFriendDto(userIdToFriend, currentUserId);
            userFriendDto.FriendshipStatus = FriendshipStatus.Friends;
            userFriendRepository.Update(userFriendDto);
            
            uow.Commit();
        }

        public void UnfriendUser(int currentUserId, int userIdToUnfriend)
        {
            var userFriendDto = GetUserFriendDto(currentUserId, userIdToUnfriend);
            userFriendDto.FriendshipStatus = FriendshipStatus.FollowsYou;
            userFriendRepository.Update(userFriendDto);

            userFriendDto = GetUserFriendDto(userIdToUnfriend, currentUserId);
            userFriendDto.FriendshipStatus = FriendshipStatus.YouFollow;
            userFriendRepository.Update(userFriendDto);

            uow.Commit();
        }


        public UserRelationsType GetUserRelationsType(int currentUserId, int otherUserId)
        {
            var userFriendDto = GetUserFriendDto(currentUserId, otherUserId);
            if (userFriendDto == null)
            {
                return UserRelationsType.NotFriends;
            }
            switch (userFriendDto.FriendshipStatus)
            {
                case FriendshipStatus.Friends:
                    return UserRelationsType.Friends;
                case FriendshipStatus.FollowsYou:
                    return UserRelationsType.FollowsYou;
                case FriendshipStatus.YouFollow:
                    return UserRelationsType.YouFollow;
                default:
                    return UserRelationsType.NotFriends;
            }
        }


        #region Private

        private IEnumerable<UserEntity> GetUsers(int currentUserId, FriendshipStatus friendshipStatus)
        {
            var requests = userFriendRepository.Get(dto => dto.UserId == currentUserId);
            var userFriendDtos = requests.Where(dto => dto.FriendshipStatus == friendshipStatus).ToList();
            var users = new List<UserEntity>(userFriendDtos.Count);
            users.AddRange(
                userFriendDtos.Select(
                    userFriendDto =>
                        userRepository.GetFirstOrDefault(dto => dto.Id == userFriendDto.FriendId).ToUserEntity()));
            return users;
        }

        private UserFriendDto GetUserFriendDto(int currentUserId, int otherUserId)
        {
            var userFriendDto =
                userFriendRepository.GetFirstOrDefault(
                    dto => dto.UserId == currentUserId && dto.FriendId == otherUserId);
            return userFriendDto;
        }

        #endregion

    }
}
