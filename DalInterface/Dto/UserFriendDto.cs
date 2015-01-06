﻿using Rtc.DalInterface.VO;

namespace Rtc.DalInterface.Dto
{
    public class UserFriendDto : IDto
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FriendId { get; set; }

        public FriendshipStatus FriendshipStatus { get; set; }

    }
}
