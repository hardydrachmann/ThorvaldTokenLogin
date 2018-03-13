using IdentityAPI.BE;
using System.Collections.Generic;
using System.Linq;

namespace IdentityAPI.DTOs
{
    public class DTOconverter
    {
        // Give a user a list of his roles using a DTO user object
        public DTOuser ConvertUser(User user)
        {
            List<Role> roles = user.UserRole.Select(r => r.Role).ToList();
            List<DTOrole> dtoRoles = new List<DTOrole>();

            foreach (var r in roles)
            {
                DTOrole dtoRole = new DTOrole
                {
                    Id = r.Id,
                    Name = r.Name
                };
                dtoRoles.Add(dtoRole);
            }

            DTOuser dtoUser = new DTOuser
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                ProfileUri = user.ProfileUri,
                IsDeleted = user.IsDeleted,
                Roles = dtoRoles
            };
            return dtoUser;
        }

        public User ConvertDTO(DTOuser user)
        {
            List<UserRole> dtoRoles = new List<UserRole>();
            if(user.Roles != null) { 
            foreach (var r in user.Roles)
            {
                UserRole dtoRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = r.Id
                };
                dtoRoles.Add(dtoRole);
            }
            }
            User dtoUser = new User
            {
                Id = user.Id,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                ProfileUri = user.ProfileUri,
                IsDeleted = user.IsDeleted,
                UserRole = dtoRoles
            };

            return dtoUser;
        }
    }
}
