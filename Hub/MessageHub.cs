using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialClint.Dto;

namespace SocialClint.Hubmo
{
  
    public class MessageHub :Hub    
    {
       
        public async Task NotifyMessage(MessageDto message)
        {
           
            await Clients.All.SendAsync("udateMessageBox", message);
            //await Clients.Users(message.RecipientId).SendAsync("udateMessageBox", message);

        }

    }
}
