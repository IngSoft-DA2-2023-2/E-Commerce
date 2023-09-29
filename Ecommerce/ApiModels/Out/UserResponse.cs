using Domain;
using System.Globalization;

namespace ApiModels.Out
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public Guid Guid { get; set; }

        public UserResponse(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
            Roles = user.Roles;
            Guid = user.Guid;
        }
    }
}
