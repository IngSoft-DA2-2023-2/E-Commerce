namespace ApiModels.In
{
    public struct UpdateUserRequestByAdmin
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public List<string> Roles { get; set; }
    }
}