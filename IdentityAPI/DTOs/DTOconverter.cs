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

        public User ConvertDTOUser(DTOuser dtoUser)
        {
            List<UserRole> dtoRoles = new List<UserRole>();
            if (dtoUser.Roles != null)
            {
                foreach (var r in dtoUser.Roles)
                {
                    UserRole dtoRole = new UserRole
                    {
                        UserId = dtoUser.Id,
                        RoleId = r.Id
                    };
                    dtoRoles.Add(dtoRole);
                }
            }
            User user = new User
            {
                Id = dtoUser.Id,
                Firstname = dtoUser.Firstname,
                Lastname = dtoUser.Lastname,
                Username = dtoUser.Username,
                Password = dtoUser.Password,
                Email = dtoUser.Email,
                ProfileUri = dtoUser.ProfileUri,
                IsDeleted = dtoUser.IsDeleted,
                UserRole = dtoRoles
            };

            return user;
        }

        public DTOrole ConvertRole(Role role)
        {
            DTOrole dtoRole = new DTOrole
            {
                Id = role.Id,
                Name = role.Name
            };
            return dtoRole;
        }

        public Role ConvertDTORole(DTOrole DtoRole)
        {
            Role role = new Role
            {
                Id = DtoRole.Id,
                Name = DtoRole.Name
            };
            return role;
        }
    }
}
