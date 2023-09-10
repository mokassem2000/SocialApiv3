using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Org.BouncyCastle.Utilities;
using SocialClint.Dto;
using SocialClint.entity;
using SocialClint.helper;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SocialClint.Hubmo
{
   [Authorize]
    public class MessageHub :Hub    
    {

        public MessageHub(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager)
        {
            UserManager = userManager;
        }
        static Dictionary<string, string> conns = new Dictionary<string, string>();

        public Microsoft.AspNetCore.Identity.UserManager<AppUser> UserManager { get; }

        public async Task NotifyMessage(MessageDto message)
           
        {


            //var connection= moconnections.SingleOrDefault(x => x.id == message.RecipientId);
            //await Clients.Client(connection.ConnectionID).SendAsync("udateMessageBox", message);
            var c = conns[message.RecipientId];
            await Clients.Client(conns[message.RecipientId]).SendAsync("udateMessageBox", message);
            //await Clients.Users(message.RecipientId).SendAsync("udateMessageBox", message);

        }
        public async override Task OnConnectedAsync()
        {
            var sub = Context.User.FindFirst("uid");
            conns.Add(sub.Value,Context.ConnectionId);
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var id = Context.User.GetUserRequestId();
            conns.Remove(id);
            var user=await UserManager.FindByIdAsync(id);
            user.LastActive = DateTime.Now;
            await UserManager.UpdateAsync(user);
            base.OnDisconnectedAsync(exception);
        }

    }   
}
