using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.Out
{
    public class CreateSessionResponse
    {
        public CreateSessionResponse() { }
        public CreateSessionResponse(Session session)
        {
            Token = session.Token;
        }

        public Guid Token { get; set; }
    }
}
