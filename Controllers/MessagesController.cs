using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialClint._services.Classes;
using SocialClint.Dto;
using SocialClint.Entities;
using SocialClint.entity;
using SocialClint.helper;
using SocialClint.Repository.Interfaces;

namespace SocialClint.Controllers
{
    [Authorize]
    public class MessagesController : ControllerBase
    {
        public MessagesController(IRepository<MemberDto> UserRepo, IMessageRepo messageRepo, IMapper mapper)
        {
            this.UserRepo = UserRepo;
            MessageRepo = messageRepo;
            Mapper = mapper;
        }

        public IRepository<MemberDto> UserRepo { get; }
        public IMessageRepo MessageRepo { get; }
        public IMapper Mapper { get; }

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


            if (await MessageRepo.saveChaengesAsync()) return Ok(Mapper.Map<MessageDto>(message));

            return BadRequest("failed to send message");

        }

        [HttpGet("messages")]
        public async Task<ActionResult<Pager<MessageDto>>> GetMEssageForUsers([FromQuery] MessageParams messageParams)
        {
            string UserId = User.GetUserRequestId();
            messageParams.UserID = UserId;
            var messages = MessageRepo.GetMessagesForUser(messageParams);
            return Ok(messages);

        }
        [HttpGet("thread/{userId}")]
        public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string userId)
        {
            var currentuserId = User.GetUserRequestId();
            var messages = MessageRepo.GetMessageThread(currentuserId, userId);
            return Ok(messages);

        }
    }
}
