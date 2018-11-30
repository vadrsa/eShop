using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities.Test;
using Modules.Test.Interfaces;
using System.Windows.Input;
using Prism.Events;
using Prism.Ioc;
using Modules.Test.Events;
using eShopUI.Infrastructure.ViewModels;

namespace Modules.Test.Messages.ViewModels
{
    class MessagesListViewModel : ObjectListViewModel<Message>
    {
        #region Private Fields
        
        private IMessageService _messageService;

        #endregion
        

        public MessagesListViewModel(IContainerExtension container) : base(container)
        {
            _messageService = container.Resolve<IMessageService>();
            ListItems = _messageService.GetAllMessages();
        }


    }
}
