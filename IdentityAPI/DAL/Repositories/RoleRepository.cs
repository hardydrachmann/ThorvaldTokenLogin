using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {

        IdentityDBEntities _entity = new IdentityDBEntities();

        public Role Create(Role entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Role entity)
        {
            throw new NotImplementedException();
        }

        public Role Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetAll()
        {
            throw new NotImplementedException();
        }

        public Role Update(Role entity)
        {
            throw new NotImplementedException();
        }
    }
}