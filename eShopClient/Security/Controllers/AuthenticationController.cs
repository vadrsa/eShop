using EntityDTO;
using EntityDTO.Users;
using eShopUI.Infrastructure.Events;
using eShopUI.Infrastructure.Interfaces;
using Prism.Events;
using Security.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Security.Controllers
{
    class AuthenticationController
    {
        private AuthenticationService _authService;
        private IEventAggregator _eventAggregator;
        public AuthenticationController(AuthenticationService authService, IEventAggregator eventAggregator)
        {
            this._authService = authService;
            this._eventAggregator = eventAggregator;
        }

        public bool Authenticate(string login, string password)
        {
            UserDTO user = this._authService.Authenticate(login, password);
            if(user != null)
            {
                (Application.Current as IApplication).User = user;
                _eventAggregator.GetEvent<AuthenticatedEvent>().Publish();
                return true;
            }
            return false;
        }
    }
}
