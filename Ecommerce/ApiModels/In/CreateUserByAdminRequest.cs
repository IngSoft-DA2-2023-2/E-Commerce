using Domain;
using Domain.ProductParts;

namespace ApiModels.In
{
    public class CreateUserByAdminRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }

        public User ToEntity()
        {
            List<StringWrapper> roles = new List<StringWrapper>();
            foreach (string role in Roles)
            {
                StringWrapper stringWrapper = new StringWrapper()
                {
                    Info = role
                };
                roles.Add(stringWrapper);
            }
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Address = Address,
                Roles = roles
            };
        }
    }
}