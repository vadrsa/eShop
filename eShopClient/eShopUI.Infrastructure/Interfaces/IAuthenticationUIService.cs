using EntityDTO;
using EntityDTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShopUI.Infrastructure.Interfaces
{
    public interface IAuthenticationUIService
    {
        void StartAuthentication();
        bool IsAuthenticated();
        UserDTO GetCurrentUser();
    }
}
