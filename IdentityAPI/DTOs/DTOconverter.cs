using IdentityAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
