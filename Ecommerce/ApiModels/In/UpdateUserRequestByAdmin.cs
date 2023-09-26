using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.In
{
    public struct UpdateUserRequestByAdmin
    {
        public string Name;
        public string Password;
        public string Address;
        public List<string> Roles;
        public string Email;
    }
}