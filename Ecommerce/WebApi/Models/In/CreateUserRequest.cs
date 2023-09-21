namespace WebApi.Models.In
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
        public string Address { get; set; }
    }
}
