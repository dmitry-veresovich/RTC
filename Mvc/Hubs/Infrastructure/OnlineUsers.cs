using System.Collections.Generic;
using System.Linq;

namespace Rtc.Mvc.Hubs.Infrastructure
{
    public class OnlineUsers
    {
        private readonly Dictionary<int, Dictionary<string, int>> users;
        private readonly object locker;

        public OnlineUsers()
        {
            users = new Dictionary<int, Dictionary<string, int>>();
            locker = new object();
        }

        public bool IsUserConnected(int userId)
        {
            return users.ContainsKey(userId);
        }


        public void ConnectUser(int userId)
        {
            lock (locker)
            {
                if (IsUserConnected(userId)) return;
                var userInfo = new Dictionary<string, int>();
                users.Add(userId, userInfo);
            }
        }

        public void ConnectUserToUser(int userId, string connectionId, int otherUserId)
        {
            lock (locker)
            {
                var userInfo = users[userId];
                userInfo[connectionId] = otherUserId;
            }
        }

        public void DisconnectUserFromUser(int userId, string connectionId)
        {
            lock (locker)
            {
                users[userId].Remove(connectionId);
            }
        }

        public string GetUserConnectionId(int userId, int otherUserId)
        {
            var userInfo = users[otherUserId];
            return (from pair in userInfo where pair.Value == userId select pair.Key).FirstOrDefault();
        }




        public int Count
        {
            get { return users.Count; }
        }


    }
}