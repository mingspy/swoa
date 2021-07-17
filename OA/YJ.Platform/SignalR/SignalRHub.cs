using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace YJ.Platform.SignalR
{
    public class SignalRHub : Hub
    {
        public void SendMessage(string message, string userID = "")
        {
            //if (userID.IsNullOrEmpty())
            //{
            //    Clients.All.ReceiveMessage(message);
            //}
            //else
            //{
            //    Clients.Group(userID.ToLower()).ReceiveMessage(message);
            //}
        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            //string userID = YJ.Platform.Users.CurrentUserID.ToString().ToLower();
            //Groups.Add(Context.ConnectionId, userID);
            return base.OnConnected();
        }
    }
}