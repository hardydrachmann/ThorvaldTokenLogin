using IdentityAPI.DAL.DAO;
using IdentityAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public class UserRepository : IRepository<DTOuser>
    {
        IServiceProvider _serviceProvider;
        DTOconverter dtoConverter;

        public UserRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            dtoConverter = new DTOconverter();
        }

        public async Task<int> Create(DTOuser entity)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                var user = dtoConverter.ConvertDTOUser(entity);
                context.User.Add(user);
                return await context.SaveChangesAsync();
            }
        }

        // Only soft delete a user in the database - therefore using below UPDATE method instead
        public Task<int> Delete(int id)
        {
            throw new NotImplementedException();
        }
       
        public async Task<DTOuser> Get(int id)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                var user = await context.User.Include(u => u.UserRole).ThenInclude(r => r.Role).FirstOrDefaultAsync(u => u.Id == id);
                return dtoConverter.ConvertUser(user);
            }
        }

        public async Task<List<DTOuser>> GetAll() { 
        
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

        public async Task<int> Update(DTOuser entity)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                context.Update(dtoConverter.ConvertDTOUser(entity));
                return await context.SaveChangesAsync();
            }
        }
        public async Task<DTOuser> GetByUsername(string username)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                var user = await context.User.Include(u => u.UserRole).ThenInclude(r => r.Role).FirstOrDefaultAsync(u => u.Username == username);
                return dtoConverter.ConvertUser(user);
            }
        }
    }
}
