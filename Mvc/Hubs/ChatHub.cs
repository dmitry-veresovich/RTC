using System.Collections.Generic;
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

        private readonly static ConnectionMapping connections = new ConnectionMapping();

        private static readonly List<int> usersOnline = new List<int>();

        private readonly IAccountService accountService = RtcDependencyResolver.GetService<IAccountService>();

        #endregion

        static ChatHub() { }



        #region Client

        public void GetFriendsOnline()
        {
            //
        }
     
        public void SendWord(string word, int userToSendId)
        {
            //var name = Context.User.Identity.Name;
            //var userId = userService.GetUser(name, LogInType.Email).Id;

            var connectionId = connections.GetConnectionId(userToSendId);
            if (connectionId == null)
            {
                //Clients.Caller.error();
            }
            else
            {
                Clients.Client(connectionId).sendWordToClient(word);
            }
        }



        #endregion

        public static bool IsUserOnline(int id)
        {
            return usersOnline.Contains(id);
        }


        public static int UsersAmountOnline { get { return usersOnline.Count; } }


        public override Task OnConnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            if (!usersOnline.Contains(userId))
            {
                usersOnline.Add(userId);
            }

            //var connectionId = Context.ConnectionId;
            //connections.Add(userId, connectionId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            usersOnline.Remove(userId);
            //connections.Remove(userId);
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            var name = Context.User.Identity.Name;
            var userId = accountService.GetAccount(name, LogInType.Email).Id;
            if (!usersOnline.Contains(userId))
            {
                usersOnline.Add(userId);
            }
            //var connectionId = Context.ConnectionId;
            //connections.Add(userId, connectionId);
            return base.OnReconnected();
        }

    }
}