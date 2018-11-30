using LinqToDB.Identity;
using LinqToDB.Mapping;

namespace BusinessEntities
{
    public class User : IdentityUser
    {
    }

    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}