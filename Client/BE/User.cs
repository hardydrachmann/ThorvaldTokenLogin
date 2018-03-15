using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Client.BE
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [MinLength(2)]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [MinLength(2)]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [MinLength(8)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Display(Name = "Profile URL")]
        [Url]
        public string ProfileUri { get; set; }

        [Display(Name ="Unauthorized?")]
        public bool IsDeleted { get; set; }

        public List<Role> Roles { get; set; }
    }
}
