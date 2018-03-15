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
        public string Firstname { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Required]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsLocal { get; set; }
        [Required]
        //[DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Profile URL")]
        public string ProfileUri { get; set; }
        [Display(Name ="Unauthorized?")]
        public bool IsDeleted { get; set; }
        public List<Role> Roles { get; set; }
    }
}
