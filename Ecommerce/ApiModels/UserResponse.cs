using Domain;

namespace ApiModels
{
    public class UserResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> Roles { get; set; }

        public UserResponse(User user)
        {
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
            Roles = user.Roles;
        }
    }
}
