using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace YJ.Platform.SignalR
{
    public class SignalRConnection : PersistentConnection
    {
        protected override Task OnConnected(IRequest request, string connectionId)
        {
            string userID = "";
            try
            {
                userID = request.Cookies[YJ.Utility.Keys.SessionKeys.UserID.ToString()].Value.ToLower();
            }
            catch
            {
            }
            if (!userID.IsNullOrEmpty())
            {
                Groups.Add(connectionId, userID);
            }
            return base.OnConnected(request, connectionId);
        }

        protected override Task OnReceived(IRequest request, string connectionId, string data)
        {
            return base.OnReceived(request, connectionId, data);
        }

        protected override Task OnReconnected(IRequest request, string connectionId)
        {
            string userID = "";
            try
            {
                userID = request.Cookies[YJ.Utility.Keys.SessionKeys.UserID.ToString()].Value.ToLower();
            }
            catch
            {
            }
            if (!userID.IsNullOrEmpty())
            {
                Groups.Add(connectionId, userID);
            }
            return base.OnReconnected(request, connectionId);
        }
    }
}