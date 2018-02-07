using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class ScopeRepository : IRepository<Scope>
    {
        IdentityDBEntities _entity = new IdentityDBEntities();

        public Scope Create(Scope entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Scope entity)
        {
            throw new NotImplementedException();
        }

        public Scope Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Scope> GetAll()
        {
            throw new NotImplementedException();
        }

        public Scope Update(Scope entity)
        {
            throw new NotImplementedException();
        }
    }
}