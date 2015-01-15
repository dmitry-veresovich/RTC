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


        #region Client

        public void ConnectToUser(int otherUserId)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;

            users.ConnectUserToUser(userId, Context.ConnectionId, otherUserId);
        }


        //public void GetFriendsOnline()
        //{
        //    //
        //}

        public void SendWord(string word, int otherUserId)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            var connectionId = users.GetUserConnectionId(userId, otherUserId);
            if (connectionId == null)
            {
                //Clients.Caller.error();
                // user is offline
            }
            else
            {
                Clients.Client(connectionId).sendWordToClient(word);
            }
        }



        #endregion

        //public static bool IsUserOnline(int id)
        //{
        //    return false;
        //}


        public static int UsersOnlineCount { get { return users.Count; } }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.ConnectUser(userId);

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.DisconnectUserFromUser(userId, Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            users.ConnectUser(userId);
            return base.OnReconnected();
        }

    }
}