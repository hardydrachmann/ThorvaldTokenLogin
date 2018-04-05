using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityAPI.DAL.Repositories
{
    public class LoginRepository
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        public User Get(string username)
        {
            return db.User.SingleOrDefault(user => user.Username == username);
        }
    }
}