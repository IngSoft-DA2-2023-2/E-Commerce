using Domain;
using Domain.ProductParts;

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
            List<string> role = new List<string>();
            foreach (StringWrapper stringW in user.Roles) role.Add(stringW.Info);
            Name = user.Name;
            Email = user.Email;
            Address = user.Address;
            Roles = role;
            Guid = user.Id;
        }
    }
}
