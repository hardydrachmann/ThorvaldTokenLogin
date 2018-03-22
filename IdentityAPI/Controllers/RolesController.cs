using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/Roles")]
    public class RolesController : Controller
    {
        IServiceProvider _serviceProvider;
        RoleRepository roleRepo;

        public RolesController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            roleRepo = new RoleRepository(_serviceProvider);
        }

        //GET: api/Login
        [HttpPut]
        public Task<int> Update([FromBody]DTOrole role)
        {
            return roleRepo.Update(role);
        }

        [HttpGet]
        public Task<List<DTOrole>> GetAll()
        {
            return roleRepo.GetAll();
        }

        [HttpGet("{id:int}")]
        public Task<DTOrole> Get(int id)
        {
            return roleRepo.Get(id);
        }

        [HttpPost]
        public Task<int> Create([FromBody]DTOrole role)
        {
            return roleRepo.Create(role);
        }

        [HttpDelete("{id:int}")]
        public Task<int> Delete(int id)
        {
            return roleRepo.Delete(id);
        }
    }
}