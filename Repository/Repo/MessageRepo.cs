using AutoMapper;
using SocialClint.DAL;
using SocialClint.Dto;
using SocialClint.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.RegularExpressions;
using SocialClint.Repository.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SocialClint._services.Classes;
using SocialClint.Migrations;
using System.Drawing.Printing;

namespace SocialClint.Repository.Repo
{
    public class MessageRepo : IMessageRepo
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public MessageRepo(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public async Task AddMessage(Message message)
        {
          
            var sender = await _context.users.Include(u => u.photos).FirstOrDefaultAsync(u => u.Id == message.Sender.Id);
            var respient = await _context.users.Include(u => u.photos).FirstOrDefaultAsync(u => u.Id == message.Recipient.Id);
            message.Sender = sender;
            message.Recipient = respient;
            _context.messages.Add(message);
        }

        public async Task<bool> DeleteMessage(int id)
        {     
           _context.messages.Remove(await _context.messages.FirstOrDefaultAsync(m=>m.Id==id));
            if (! (await _context.SaveChangesAsync() > 0))
            {
                return false;
            }
            return true;
        }

        public Task<Connection> GetConnection(string connectionId)
        {
            throw new NotImplementedException();
        }

        public Task<Message> GetMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Group> GetMessageGroup(string groupName)
        {
            throw new NotImplementedException();
        }

        //public async Task<Pager<MessageDto>> GetMessagesForUser(MessageParams messageParams)
         public async Task< IEnumerable<MessageDto>> GetMessagesForUser(MessageParams messageParams,string Id)
        {
            Message[] rslt;
            switch (messageParams.Container)
            {
                case "Outbox":
                    rslt = _context.messages.Include(m => m.Sender).ThenInclude(s => s.photos).Where(m => m.Recipient.Id == messageParams.UserID && m.Sender.Id == Id).ToArray();
                    break;
                case "inbox":
                    rslt= _context.messages.Include(m => m.Sender).ThenInclude(s=>s.photos).Where(m => m.Sender.Id == messageParams.UserID && m.Recipient.Id==Id).ToArray();
                    break;
                default:
                    rslt= _context.messages.Include(m => m.Sender).ThenInclude(s => s.photos).Where(m => m.Sender.Id == messageParams.UserID && m.DateRead == null).ToArray(); ;
                    break;
            }

            var messages =rslt.ToArray();
            var messageCount = messages.Count();
            //var pager = new Pager<MessageDto>(messageCount, messageParams.PageNumber, messageParams.PageSize < 1 ? 1 : messageParams.PageSize);
            //pager.Items = messages.Skip(((messageParams.PageNumber - 1) * messageParams.PageSize)).Take(messageParams.PageSize); ;
            
            var finalMessages= _mapper.Map<MessageDto[]>(messages);

            return finalMessages;


        }

        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserId, string recipientUserId)
        {
            var messages = await _context.messages
                .Include(u => u.Sender).ThenInclude(u => u.photos)
                .Include(m => m.Recipient).ThenInclude(u => u.photos)
                .Where(m => m.Recipient.Id == currentUserId && m.Sender.Id == recipientUserId
                            ||
                            m.Sender.Id == currentUserId && m.Recipient.Id == recipientUserId
                                                                    )
                .OrderBy(m => m.MessageSent)
                .ToListAsync();

            var unreadMessage = messages.Where(m => m.DateRead == null && m.Recipient.Id == currentUserId).ToList();

            if (unreadMessage.Any())
            {
                foreach (var item in unreadMessage)
                {
                    item.DateRead = DateTime.Now;
                }
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<IEnumerable<MessageDto>>(messages);   
        }

        public void RemoveConnection(Connection connection)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> saveChaengesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
