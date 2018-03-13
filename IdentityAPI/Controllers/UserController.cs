using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace IdentityAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [Route("api/User")]
    public class UserController : Controller
    {
        IServiceProvider _serviceProvider;
        UserRepository userRepo;

        public UserController(IServiceProvider serviceProvider)
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
            user.IsDeleted = true;
            return userRepo.Update(user);
        }
    }
}