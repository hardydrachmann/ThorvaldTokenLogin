using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using IdentityAPI;
using IdentityAPI.DAL.Repositories;

namespace IdentityAPI.Controllers
{
    public class UsersController : ApiController
    {
        UserRepository userRepo = new UserRepository();

        // GET: api/Users
        [HttpGet]
        public IQueryable<User> GetUsers()
        {
            return userRepo.GetAll();
        }

        //GET: api/Users/5
        [HttpGet]
        public User GetUser(int id)
        {
            return userRepo.Get(id);
        }

        // PUT: api/Users/{user}
        [HttpPut]
        public User PutUser(User u)
        {
            return userRepo.Update(u);
        }

        // PUT: api/Users/5
        [HttpPut]
        public bool IsDeletedUser(int id)
        {
            return userRepo.IsDeleted(id);
        }

        // POST: api/Users
        [HttpPost]
        public User PostUser(User u)
        {
            return userRepo.Create(u);
        }

    }
}