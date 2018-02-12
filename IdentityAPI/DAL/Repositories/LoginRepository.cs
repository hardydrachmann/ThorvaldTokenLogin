using IdentityAPI.DTOs;
using IdentityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public class LoginRepository
    {
        IServiceProvider _serviceProvider;
        DTOconverter dtoConverter = new DTOconverter();

        public LoginRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<UserValidation> GetUserValidationByUsername(string username)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                UserValidation userValidation = new UserValidation();

                var user = await context.User.SingleOrDefaultAsync(u => u.Username == username);

                userValidation.Id = user.Id;
                userValidation.Username = user.Username;
                userValidation.Password = user.Password;
                return userValidation;
            }
        }

        public async Task<DTOuser> GetUserByUserId(int id)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                DTOuser dtoUser = new DTOuser();

                var user = await context.User.Include(u => u.UserRole).ThenInclude(r => r.Role).FirstOrDefaultAsync(u => u.Id == id);

                return dtoConverter.ConvertUser(user);
            }
        }
    }
}
