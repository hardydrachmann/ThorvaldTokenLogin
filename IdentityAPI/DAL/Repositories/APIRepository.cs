using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class APIRepository : IRepository<API>
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public API Create(API a)
        {
            db.API.Add(a);
            db.SaveChanges();
            return a;
        }

        public bool Delete(API a)
        {
            try
            {
                db.API.Remove(a);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public API Get(int id)
        {
            return db.API.Find(id);
        }

        public IQueryable<API> GetAll()
        {
            return db.API;
        }

        public API Update(API a)
        {
            db.Entry(a).State = EntityState.Modified;
            db.SaveChanges();
            return a;
        }
    }
}