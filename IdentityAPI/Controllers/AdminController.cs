using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    public class AdminController : Controller
    {
        IServiceProvider _serviceProvider;
        AdminRepository adminRepo;


        public AdminController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            adminRepo = new AdminRepository(_serviceProvider);
        }

        // GET api/values
        [HttpGet]
        public Task<List<DTOuser>> Get()
        {
            return adminRepo.GetAll();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/username
        [Authorize(Roles = "Admin")]
        [HttpPut("{username}")]
        public Task<int> Put(string username)
        {
           return adminRepo.UpdatUsername(username);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
