using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using IdentityAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAPI.DAL.Repositories
{
    public class UserRepository : IRepository<DTOuser>
    {
        IServiceProvider _serviceProvider;
        DTOconverter DTOconverter = new DTOconverter();

        public UserRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<DTOuser> Create(DTOuser entity)
        {
            throw new NotImplementedException();
        }

        public Task<DTOuser> Delete(DTOuser entity)
        {
            throw new NotImplementedException();
        }

        public Task<DTOuser> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<DTOuser>> GetAll()
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                List<DTOuser> dtoUsers = new List<DTOuser>();

                var users = await context.User.Include(u => u.UserRole).ThenInclude(r => r.Role).ToListAsync();

                foreach (var user in users)
                {
                    dtoUsers.Add(DTOconverter.ConvertUser(user));
                }
                return dtoUsers;
            }
        }

        public Task<DTOuser> Update(DTOuser entity)
        {
            throw new NotImplementedException();
        }
    }
}
