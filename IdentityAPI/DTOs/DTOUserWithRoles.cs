using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.DTOs
{
    public class DTOUserWithRoles
    {
        public DTOuser User { get; set; }
        public List<DTOrole> Roles { get; set; }
    }
}
