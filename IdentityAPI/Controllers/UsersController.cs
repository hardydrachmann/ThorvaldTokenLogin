using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityAPI.DAL.Repositories;
using IdentityAPI.DTOs;
using Microsoft.AspNetCore.Authorization;
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
        UserRoleRepository userRoleRepo;

        public UsersController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            userRepo = new UserRepository(_serviceProvider);
            userRoleRepo = new UserRoleRepository(_serviceProvider);
        }

        //GET: api/users
        [HttpPut]
        public async Task<int> Update([FromBody]DTOUserWithRoles dtoUserRoles)
        {
            if (dtoUserRoles.Roles != null)
            {
                userRoleRepo.Delete(dtoUserRoles.User.Id);

                foreach (var item in dtoUserRoles.Roles)
                {
                    DTOUserRole dtoUserRole = new DTOUserRole();
                    dtoUserRole.RoleId = item.Id;
                    dtoUserRole.Role = item;
                    dtoUserRole.UserId = dtoUserRoles.User.Id;
                    dtoUserRole.User = dtoUserRoles.User;

                    userRoleRepo.Create(dtoUserRole);
                }
            }
            return await userRepo.Update(dtoUserRoles.User);
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
        public async Task<int> Create([FromBody]DTOUserWithRoles userWithRole)
        {
            userRepo.Create(userWithRole.User);

            var user = userRepo.GetByUsername(userWithRole.User.Username).Result;

            foreach (var item in userWithRole.Roles)
            {
                DTOUserRole dtoUserRole = new DTOUserRole();
                dtoUserRole.RoleId = item.Id;
                dtoUserRole.Role = item;
                dtoUserRole.UserId = user.Id;
                dtoUserRole.User = user;

                userRoleRepo.Create(dtoUserRole);
            }
            return 1;
        }
    }
}