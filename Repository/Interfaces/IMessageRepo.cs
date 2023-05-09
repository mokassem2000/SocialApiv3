using SocialClint.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text.RegularExpressions;
using SocialClint.Dto;
using SocialClint._services.Classes;

namespace SocialClint.Repository.Interfaces
{
    public interface IMessageRepo
    {

        Task AddMessage(Message message);
        void DeleteMessage(Message message);
        Task<Message> GetMessage(int id);
        void AddGroup(Group group);
        Task<Pager<MessageDto>> GetMessagesForUser(MessageParams messageParams);
        Task<IEnumerable<MessageDto>> GetMessageThread(string currentUserName, string recipientUserName);
        void RemoveConnection(Connection connection);
        Task<Connection> GetConnection(string connectionId);
        Task<Group> GetMessageGroup(string groupName);
        Task<bool> saveChaengesAsync();

    }
}
