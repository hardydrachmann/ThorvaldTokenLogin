﻿using System;
using System.Collections.Generic;

namespace IdentityAPI.Models
{
    public partial class User
    {
        public User()
        {
            UserRole = new HashSet<UserRole>();
        }

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

        public ICollection<UserRole> UserRole { get; set; }
    }
}
