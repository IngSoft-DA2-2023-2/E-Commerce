using Domain;

namespace ApiModels.Out
{
    public class SessionResponse
    {
        public SessionResponse() { }
        public SessionResponse(Session session)
        {
            Token = session.Id;
        }

        public Guid Token { get; set; }
    }
}
