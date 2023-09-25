using Domain;

namespace ApiModels.In
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }

        public User ToEntity()
        {
            return new User
            { 
                Name = Name, 
                Email = Email,
                Password = Password,
                Address = Address,
                Roles = Roles
            };
        }
    }
}
