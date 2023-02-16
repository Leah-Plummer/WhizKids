using System.Collections.Generic;
using WhizKids.Models;

namespace WhizKids.Repositories
{
    public interface IMessageRepository
    {
        List<Message> GetAllMessages();
        List<Message> GetAllMessagesById(int id);
        Message GetMessageById(int id);
        void AddMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(int id);
    }
}
