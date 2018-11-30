using BusinessEntities.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Test.Interfaces
{
    interface IMessageService
    {
        List<Message> GetAllMessages();
        Message GetMessageByID(int id);
    }
}
