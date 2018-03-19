using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.DTOs
{
    public class DTOUserRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public DTOrole Role { get; set; }
        public DTOuser User { get; set; }
    }
}
