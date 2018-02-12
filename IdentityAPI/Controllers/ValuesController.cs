using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        IServiceProvider _serviceProvider;
        UserRepository userRepo;


        public ValuesController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            userRepo = new UserRepository(_serviceProvider);
        }

        // GET api/values
        [HttpGet]
        public Task<List<DTOuser>> Get()
        {
            return userRepo.GetAll();
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
