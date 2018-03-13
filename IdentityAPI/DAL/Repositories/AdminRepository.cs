using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityAPI.DAL.DAO;
using IdentityAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityAPI.DAL.Repositories
{
    public class AdminRepository : IRepository<DTOuser>
    {
        IServiceProvider _serviceProvider;
        DTOconverter dtoConverter = new DTOconverter();

        public AdminRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task<int> Create(DTOuser entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> Delete(int id)
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
                    dtoUsers.Add(dtoConverter.ConvertUser(user));
                }
                return dtoUsers;
            }
        }

        public async Task<int> UpdatUsername(string username)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                string newUsername = "FUCK!!!";
                var result = await context.User.SingleOrDefaultAsync(u => u.Username == "hd");
                result.Username = newUsername;
                int statusCode = await context.SaveChangesAsync();
                return statusCode;
            }
        }

        public Task<int> Update(DTOuser entity)
        {
            throw new NotImplementedException();
        }
    }
}
