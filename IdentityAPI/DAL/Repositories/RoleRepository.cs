using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public Role Create(Role r)
        {
            db.Role.Add(r);
            db.SaveChanges();
            return r;
        }

        public bool Delete(Role r)
        {
            try
            {
                db.Role.Remove(r);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Role Get(int id)
        {
            return db.Role.Find(id);
        }

        public IQueryable<Role> GetAll()
        {
            return db.Role;
        }

        public Role Update(Role r)
        {
            db.Entry(r).State = EntityState.Modified;
            db.SaveChanges();
            return r;
        }
    }
}