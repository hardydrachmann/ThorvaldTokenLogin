using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        IdentityDBEntities _entity = new IdentityDBEntities();

        public Client Create(Client entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public Client Get(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Client> GetAll()
        {
            throw new NotImplementedException();
        }

        public Client Update(Client entity)
        {
            throw new NotImplementedException();
        }
    }
}