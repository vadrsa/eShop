using eShopUI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShopUI.Infrastructure.Interfaces;
using Prism.Ioc;
using Modules.Test.Messages.Views;
using Modules.Test.Constants;
using Modules.Test.Messages.ViewModels;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using eShopUI.Infrastructure.DevExpress;
using BusinessEntities.Test;

namespace Modules.Test.Messages
{
    class MessageWorkItem : ObjectManagerWorkItem<MessagesListView, 
                                                        MessagesDetailView, 
                                                        Message>
    {
        public MessageWorkItem(IContainerExtension container) : base(container)
        {
        }

        public override string WorkItemName
        {
            get
            {
                return UIConstants.MessageWorkItemName;
            }
        }

        protected override void AddAdditionalCommandGroups()
        {
            base.AddAdditionalCommandGroups();
            
        }

        public override void Run()
        {
            base.Run();
            
        }
    }
}
