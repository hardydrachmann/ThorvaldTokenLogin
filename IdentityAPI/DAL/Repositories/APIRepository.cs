using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class APIRepository : IRepository<API>
    {
        IdentityDBEntities _entity = new IdentityDBEntities();

        public API Create(API entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(API entity)
        {
            throw new NotImplementedException();
        }

        public API Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<API> GetAll()
        {
            throw new NotImplementedException();
        }

        public API Update(API entity)
        {
            throw new NotImplementedException();
        }
    }
}