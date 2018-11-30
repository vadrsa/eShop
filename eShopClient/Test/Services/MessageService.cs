using Modules.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Test;

namespace Modules.Test.Services
{
    class MessageService : IMessageService
    {
        private List<Message> messages = new List<Message>() {
            new Message {ID = 1, Content = "Hello World!" },
            new Message {ID = 2, Content = "Hello World2!" },
            new Message {ID = 3, Content = "Hello World3!" },
        };

        public List<Message> GetAllMessages()
        {
            return messages;
        }

        public Message GetMessageByID(int id)
        {
            return messages.Where(m => m.ID == id).FirstOrDefault();    
        }
    }
}
