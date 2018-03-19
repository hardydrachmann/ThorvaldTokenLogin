using IdentityAPI.DAL.DAO;
using IdentityAPI.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityAPI.DAL.Repositories
{
    public class UserRoleRepository : IRepository<DTOUserRole>
    {
        IServiceProvider _serviceProvider;
        DTOconverter dtoConverter;

        public UserRoleRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            dtoConverter = new DTOconverter();
        }

        public async Task<int> Create(DTOUserRole entity)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                context.UserRole.Add(dtoConverter.ConvertDTOUserRole(entity));
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                context.UserRole.RemoveRange(context.UserRole.Where(u => u.UserId == id));
                return await context.SaveChangesAsync();
            }
        }

        public Task<DTOUserRole> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<DTOUserRole>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(DTOUserRole entity)
        {

            throw new NotImplementedException();
        }
    }
}