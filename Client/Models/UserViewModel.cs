using Client.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<Role> Roles { get; set; }
    }
}
