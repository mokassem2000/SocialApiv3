using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialClint._services.Classes;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;
using SocialClint.helper;
using SocialClint.Hubmo;
using SocialClint.Repository.Interfaces;

namespace SocialClint.Controllers
{

    public class MessagesController : ControllerBase
    {
        public MessagesController(IRepository<MemberDto> UserRepo, IMessageRepo messageRepo, IMapper mapper, IHubContext<MessageHub> hubContext)
        {
            this.UserRepo = UserRepo;
            MessageRepo = messageRepo;
            Mapper = mapper;
            HubContext = hubContext;
        }

        public IRepository<MemberDto> UserRepo { get; }
        public IMessageRepo MessageRepo { get; }
        public IMapper Mapper { get; }
        public IHubContext<MessageHub> HubContext { get; }

        [HttpPost("createMessage")]
        public async Task<ActionResult<MessageDto>> CreateMessage([FromBody] CreateMessageDto createmessageDto)
        {
            var uid = User.GetUserRequestId();
            var senderuser = await UserRepo.GetByIdAsync(uid);
            if (senderuser.MemberId == createmessageDto.RecipientUserId) return BadRequest("you cant sent message to yuourself!");
            var recipientuser = await UserRepo.GetByIdAsync(createmessageDto.RecipientUserId);
            if (recipientuser is null) return NotFound();
           
            var message = new Message()
            {
                Sender = Mapper.Map<AppUser>(senderuser),
                Recipient = Mapper.Map<AppUser>(recipientuser),
                SenderUsername = Mapper.Map<AppUser>(senderuser).UserName,
                RecipientUsername = Mapper.Map<AppUser>(recipientuser).UserName,
                Content = createmessageDto.content
            };
            await MessageRepo.AddMessage(message);



            if (await MessageRepo.saveChaengesAsync())
            {
                //await HubContext.Clients.All.SendAsync("udateMessageBox", Mapper.Map<MessageDto>(message));
                //await  hubContext.Clients.Group(message.RecipientId).SendAsync("udateMessageBox", Mapper.Map<MessageDto>(message));
                return Ok(Mapper.Map<MessageDto>(message));

            }
                
                

            return BadRequest("failed to send message");

        }

        [HttpGet("messages")]
        public async Task<ActionResult<Pager<MessageDto>>> GetMEssageForUsers([FromQuery] MessageParams messageParams)
        {
            string Id = User.GetUserRequestId();        
            var messages = MessageRepo.GetMessagesForUser(messageParams,Id);
            return Ok(messages);

        }
        [HttpGet("thread/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string userId)
        {
            var currentuserId = User.GetUserRequestId();
            var messages = await MessageRepo.GetMessageThread(currentuserId, userId);
            return Ok(messages);

        }



        [HttpDelete("message/delete/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            if ( !await MessageRepo.DeleteMessage(id)) {
                return BadRequest();
            }
            return Ok();
        }
    }
}
