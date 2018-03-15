using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Authorize(Roles = "Admin")]
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

        //GET: api/users
        [HttpPut]
        public Task<int> Update([FromBody]DTOuser user)
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
        public Task<int> Create([FromBody]DTOuser user)
        {
            return userRepo.Create(user);
        }
    }
}