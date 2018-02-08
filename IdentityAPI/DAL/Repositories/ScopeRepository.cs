using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class ScopeRepository : IRepository<Scope>
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public Scope Create(Scope s)
        {
            db.Scope.Add(s);
            db.SaveChanges();
            return s;
        }

        public bool Delete(Scope s)
        {
            try
            {
                db.Scope.Remove(s);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Scope Get(int id)
        {
            return db.Scope.Find(id);
        }

        public IQueryable<Scope> GetAll()
        {
            return db.Scope;
        }

        public Scope Update(Scope s)
        {
            db.Entry(s).State = EntityState.Modified;
            db.SaveChanges();
            return s;
        }
    }
}