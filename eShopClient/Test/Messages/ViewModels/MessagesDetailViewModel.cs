using BusinessEntities.Test;
using DevExpress.Mvvm;
using eShopUI.Infrastructure.ViewModels;
using Modules.Test.Events;
using Modules.Test.Interfaces;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modules.Test.Messages.ViewModels
{
    class MessagesDetailViewModel : ObjectDetailViewModel<Message>
    {
        #region Private Fields
        
        private IMessageService _messageService;

        #endregion

        public MessagesDetailViewModel(IContainerExtension container) : base(container)
        {
            EventAggregator.GetEvent<MessageListViewRowChanged>().Subscribe(HandleListViewRowChanged);
            _messageService = container.Resolve<IMessageService>();
        }

        #region Public/Protected Methods

        protected override void HandleListViewRowChanged(int id)
        {
            Object = _messageService.GetMessageByID(id);
        }

        #endregion
    }
}
