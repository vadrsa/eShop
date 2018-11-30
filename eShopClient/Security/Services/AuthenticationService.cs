using EntityDTO;
using EntityDTO.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Services
{
    public class AuthenticationService
    {

        public UserDTO Authenticate(string login, string pass)
        {
            if (login == "admin" && pass == "123")
            {
                return new UserDTO() { ID = "07b55a42-ca9d-460f-bcb7-25d6e8bb3439", UserName = "admin"};
            }
            return null;
        }
        
    }
}
