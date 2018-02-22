using System.Collections.Generic;

namespace IdentityAPI.BE
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserRole> UserRole { get; set; }
    }
}
