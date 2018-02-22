using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        IServiceProvider _serviceProvider;
        LoginRepository loginRepo;

        public LoginController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            loginRepo = new LoginRepository(_serviceProvider);
        }

        //GET: api/Login?username=string username
        [HttpGet("{username}")]
        public Task<UserValidation> GetUserValidationByUsername(string username)
        {
            return loginRepo.GetUserValidationByUsername(username);
        }

        //GET: api/Login?id=int id
        [HttpGet("{id:int}")]
        public Task<DTOuser> GetUserByUserId(int id)
        {
            return loginRepo.GetUserByUserId(id);
        }

        //GET: api/Login
        [HttpGet]
        public Task<List<DTOuser>> GetAllUsers()
        {
            return loginRepo.GetAllUsers();
        }
    }
}