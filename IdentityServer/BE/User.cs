using System;
using System.Collections.Generic;

namespace IdentityServer.BE
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsLocal { get; set; }
        public DateTime BirthDate { get; set; }
        public string ProfileUri { get; set; }
        public bool IsDeleted { get; set; }
        public List<Role> Roles { get; set; }
    }
}
