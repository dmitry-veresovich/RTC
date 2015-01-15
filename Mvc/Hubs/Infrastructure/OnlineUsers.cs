using System.Collections.Generic;

namespace Rtc.Mvc.Hubs.Infrastructure
{
    public class OnlineUsers
    {
        private readonly Dictionary<int, UserInfo> users;
        private readonly object locker;

        public OnlineUsers()
        {
            users = new Dictionary<int, UserInfo>();
            locker = new object();
        }

        public bool IsUserConnected(int userId)
        {
            return users.ContainsKey(userId);
        }


        public void ConnectUser(int userId, string connectionId)
        {
            lock (locker)
            {
                if (IsUserConnected(userId))
                {
                    users[userId].ConnectionId = connectionId;
                }
                else
                {
                    var userInfo = new UserInfo(connectionId);
                    users.Add(userId, userInfo);
                }
            }
        }

        public void ConnectUserToUser(int userId, int otherUserId)
        {
            lock (locker)
            {
                var userInfo = users[userId];
                userInfo.ConnectedUserId = otherUserId;
            }
        }

        public void DisconnectUserFromUsers(int userId)
        {
            lock (locker)
            {
                users[userId].ConnectedUserId = null;
            }
        }

        public void DisconnectUser(int userId)
        {
            lock (locker)
            {
                users.Remove(userId);
            }
        }


        public string GetUserConnectionId(int userId)
        {
            return IsUserConnected(userId) ? users[userId].ConnectionId : null;
        }

        public int Count
        {
            get { return users.Count; }
        }


    }
}