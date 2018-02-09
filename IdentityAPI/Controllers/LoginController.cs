using IdentityAPI.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityAPI.Controllers
{
    public class LoginController : ApiController
    {
        LoginRepository userRepo = new LoginRepository();

        //GET: api/Login?username=string username
        [HttpGet]
        public User GetUserByUsername(string username)
        {
            return userRepo.Get(username);
        }
    }
}
