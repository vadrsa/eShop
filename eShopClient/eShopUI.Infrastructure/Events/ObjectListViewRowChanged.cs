﻿using eShopUI.Infrastructure.Enums;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Events
{
    class ObjectListViewRowChanged : PubSubEvent<int>
    {
    }
}
