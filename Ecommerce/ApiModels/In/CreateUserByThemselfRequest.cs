using Domain;

namespace ApiModels.In
{
    public class CreateUserByThemselfRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Address = Address,
            };
        }
    }
}