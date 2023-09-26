using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.In
{
    public class CreateSessionRequest
    {
        public string Email {  get; set; }
        public string Password { get; set; }
    }
}
