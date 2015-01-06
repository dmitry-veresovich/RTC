using System.Collections.Generic;

namespace Rtc.Mvc.Hubs.Infrastructure
{
    public class ConnectionMapping
    {
        private readonly Dictionary<int, string> connections = new Dictionary<int, string>();


        public int Count
        {
            get { return connections.Count; }
        }

        public bool Add(int userId, string connectionId)
        {
            lock (connections)
            {
                if (connections.ContainsKey(userId))
                {
                    return false;
                }
                connections.Add(userId, connectionId);
                return true;
            }
        }

        public string GetConnectionId(int userId)
        {
            string connectionId;
            return connections.TryGetValue(userId, out connectionId) ? connectionId : null;
        }

        public void Remove(int userId)
        {
            lock (connections)
            {
                connections.Remove(userId);
            }
        }
    }
}
