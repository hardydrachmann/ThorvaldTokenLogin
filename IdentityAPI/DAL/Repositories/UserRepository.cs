using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public User Create(User u)
        {
            db.User.Add(u);
            db.SaveChanges();
            return u;
        }

        public bool Delete(User u)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            return db.User.Find(id);
        }

        public IQueryable<User> GetAll()
        {
            return db.User;
        }

        public User Update(User u)
        {
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();
            return u;
        }

        public bool UserExsists(User u)
        {
            return db.User.Count(e => e.Id == u.Id) > 0;
        }
    }
}