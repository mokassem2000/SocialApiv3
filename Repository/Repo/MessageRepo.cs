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

        public void DeleteMessage(Message message)
        {
            throw new NotImplementedException();
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

        public async Task<Pager<MessageDto>> GetMessagesForUser(MessageParams messageParams)
        {
            var qry = _context.messages.OrderByDescending(m => m.MessageSent).AsQueryable();
            switch (messageParams.Container)
            {
                case "inbox":
                    qry.Where(m => m.Recipient.Id == messageParams.UserID);
                    break;
                case "outbox":
                    qry.Where(m => m.Sender.Id == messageParams.UserID);
                    break;
                default:
                    qry.Where(m => m.Sender.Id == messageParams.UserID && m.DateRead == null);
                    break;
            }

            var messages = _mapper.ProjectTo<MessageDto>(qry, _mapper.ConfigurationProvider);
            var messageCount = messages.Count();
            var pager = new Pager<MessageDto>(messageCount, messageParams.PageNumber, messageParams.PageSize < 1 ? 1 : messageParams.PageSize);
            pager.Items = messages.Skip(((messageParams.PageNumber - 1) * messageParams.PageSize)).Take(messageParams.PageSize); ;
            return pager;


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
