using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        IServiceProvider _serviceProvider;
        UserRepository userRepo;

        public UsersController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            userRepo = new UserRepository(_serviceProvider);
        }

        //GET: api/Login
        [HttpPut]
        public Task<int> Update(DTOuser user)
        {
            return userRepo.Update(user);
        }

        [HttpGet]
        public Task<List<DTOuser>> GetAll()
        {
            return userRepo.GetAll();
        }

        [HttpGet("{id:int}")]
        public Task<DTOuser> Get(int id)
        {
            return userRepo.Get(id);
        }

        [HttpPost]
        public Task<int> Create(DTOuser user)
        {
            return userRepo.Create(user);
        }

        [HttpDelete]
        public Task<int> Delete(DTOuser user)
        {
            return userRepo.Delete(user);
        }
    }
}