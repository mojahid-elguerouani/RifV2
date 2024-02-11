using ChatApp.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Domain.Abstract
{
    public interface IMessage
    {
        ChatMessage SaveChatMessage(ChatMessage objentity);
        MessageRecords GetChatMessagesByUserID(string currentUserID, string toUserID, int lastMessageID = 0);
        void UpdateMessageStatusByUserID(string fromUserID, string currentUserID);
        void UpdateMessageStatusByMessageID(int messageID);
    }
}
