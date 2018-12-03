using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Endpoints.Controllers
{
    public abstract class ApiControllerBase : ControllerBase
    {
        private IServiceProvider _serviceProvider;
        protected IServiceProvider ServiceProvider { get { return _serviceProvider; } }

        public ApiControllerBase(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }
    }
}
