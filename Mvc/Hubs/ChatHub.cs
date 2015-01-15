using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Rtc.BllInterface.Services;
using Rtc.BllInterface.VO;
using Rtc.Mvc.Hubs.Infrastructure;
using Rtc.Mvc.Infrastructure;

namespace Rtc.Mvc.Hubs
{
    [Microsoft.AspNet.SignalR.Authorize]
    public class ChatHub : Hub
    {
        #region Private

        private readonly static OnlineUsers users = new OnlineUsers();

        private readonly IAccountService accountService = RtcDependencyResolver.GetService<IAccountService>();

        #endregion

        static ChatHub() { }
        public static bool IsUserOnline(int id)
        {
            return users.IsUserConnected(id);
        }

        #region Client

        public void ConnectToUser(int otherUserId)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;

            users.ConnectUserToUser(userId, otherUserId);
        }


        //public void GetFriendsOnline()
        //{
        //    //
        //}

        public void SendWord(string word, int otherUserId)
        {
            var connectionId = users.GetUserConnectionId(otherUserId);
            if (connectionId == null)
            {
                var name = Context.User.Identity.Name;
                var userId = accountService.GetAccount(name, LogInType.Email).Id;
                Clients.Client(users.GetUserConnectionId(userId)).userIsOffline();
            }
            else
            {
                Clients.Client(connectionId).sendWordToClient(word);
            }
        }

        public void ChechkUserOnline(int userId)
        {

        }



        #endregion


        public static int UsersOnlineCount { get { return users.Count; } }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.ConnectUser(userId, Context.ConnectionId);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.ConnectUser(userId, Context.ConnectionId);
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.DisconnectUser(userId);
            return base.OnDisconnected(stopCalled);
        }

    }
}