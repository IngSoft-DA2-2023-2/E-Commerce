namespace Domain
{
    public class Session
    {
        public Guid Id { get; set; }
        public virtual User User { get; set; }

    }
}
