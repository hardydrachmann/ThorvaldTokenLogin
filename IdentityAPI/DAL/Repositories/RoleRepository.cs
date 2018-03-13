using IdentityAPI.BE;
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
    public class RoleRepository : IRepository<DTOrole>
    {
        IServiceProvider _serviceProvider;
        DTOconverter dtoConverter;

        public RoleRepository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            dtoConverter = new DTOconverter();
        }

        public async Task<int> Create(DTOrole entity)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                var role = dtoConverter.ConvertDTORole(entity);
                context.Role.Add(role);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> Delete(int id)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                DTOrole role = new DTOrole() { Id = id };
                context.UserRole.RemoveRange(context.UserRole.Where(i => i.RoleId == id));
                context.Role.Remove(dtoConverter.ConvertDTORole(role));
                return await context.SaveChangesAsync();
            }
        }

        public async Task<DTOrole> Get(int id)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                var role = await context.Role.FirstOrDefaultAsync(u => u.Id == id);
                return dtoConverter.ConvertRole(role);
            }
        }

        public async Task<List<DTOrole>> GetAll()
        {

            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                List<DTOrole> dtoRoles = new List<DTOrole>();

                var roles = await context.Role.ToListAsync();

                foreach (var role in roles)
                {
                    dtoRoles.Add(dtoConverter.ConvertRole(role));
                }
                return dtoRoles;
            }
        }

        public async Task<int> Update(DTOrole entity)
        {
            using (var context = new ThorvaldIdentityDBContext(_serviceProvider.GetRequiredService<DbContextOptions<ThorvaldIdentityDBContext>>()))
            {
                context.Role.Update(dtoConverter.ConvertDTORole(entity));

                return await context.SaveChangesAsync();
            }
        }
    }
}
