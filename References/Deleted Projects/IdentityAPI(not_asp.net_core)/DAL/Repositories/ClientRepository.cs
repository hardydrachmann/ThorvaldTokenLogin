using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public Client Create(Client c)
        {
            db.Client.Add(c);
            db.SaveChanges();
            return c;
        }

        public bool Delete(Client c)
        {
            try
            {
                db.Client.Remove(c);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Client Get(int id)
        {
            return db.Client.Find(id);
        }

        public IQueryable<Client> GetAll()
        {
            return db.Client;
        }

        public Client Update(Client c)
        {
            db.Entry(c).State = EntityState.Modified;
            db.SaveChanges();
            return c;
        }
    }
}