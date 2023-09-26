using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiModels.In
{
    public class UpdateUserRequestByThemself
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Name = Name,
                Password = Password,
                Address = Address,
            };
        }
    }
}
