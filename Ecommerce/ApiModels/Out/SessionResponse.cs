using Domain;

namespace ApiModels.Out
{
    public class SessionResponse
    {
        public SessionResponse() { }
        public SessionResponse(Session session)
        {
            Token = session.Id;
            User = new UserResponse(session.User);
            
        }

        public Guid Token { get; set; }
        public UserResponse User { get; set; }
        
    }
}
