using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Endpoints.DEV
{
    public class LagResponseAttribute : ActionFilterAttribute
    {
        private static readonly Random Rand = new Random();

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            Thread.Sleep(TimeSpan.FromSeconds(Rand.Next(2,3)));
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
