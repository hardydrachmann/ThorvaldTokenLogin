using System;
using System.Collections.Generic;

namespace IdentityAPI.DTOs
{
    public class DTOuser
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfileUri { get; set; }
        public bool IsDeleted { get; set; }

        public List<DTOrole> Roles { get; set; }
    }
}
