using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialClint.Dto;
using SocialClint.helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SocialClint.Hubmo
{
   [Authorize]
    public class MessageHub :Hub    
    {
        public async Task NotifyMessage(MessageDto message)
        {
            //var connection= moconnections.SingleOrDefault(x => x.id == message.RecipientId);
            //await Clients.Client(connection.ConnectionID).SendAsync("udateMessageBox", message);
            await Clients.Group(message.RecipientId).SendAsync("udateMessageBox", message);
            //await Clients.Users(message.RecipientId).SendAsync("udateMessageBox", message);

        }
        public async override Task OnConnectedAsync()
        {
            var sub = Context.User.FindFirst("uid");
            await Groups.AddToGroupAsync(Context.ConnectionId,sub.Value);
            await base.OnConnectedAsync();
        }
        //public override async Task OnDisconnectedAsync(Exception? exception)
        //{
        //    var id = Context.User.GetUserRequestId();
        //    await Groups.RemoveFromGroupAsync(Context.ConnectionId, id);
        //    base.OnDisconnectedAsync(exception);
        //}

    }
}
