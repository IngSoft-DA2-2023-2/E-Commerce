using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
