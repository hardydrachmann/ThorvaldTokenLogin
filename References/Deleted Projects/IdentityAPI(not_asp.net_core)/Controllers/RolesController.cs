using System.Linq;
using System.Web.Http;
using IdentityAPI.DAL.Repositories;

namespace IdentityAPI.Controllers
{
    public class RolesController : ApiController
    {
        RoleRepository roleRepo = new RoleRepository();

        // GET: api/Roles
        [HttpGet]
        public IQueryable<Role> GetRoles()
        {
            return roleRepo.GetAll();
        }

        //GET: api/Roles/5
        [HttpGet]
        public Role GetRole(int id)
        {
            return roleRepo.Get(id);
        }

        // PUT: api/Roles/{role}
        [HttpPut]
        public Role PutRole(Role r)
        {
            return roleRepo.Update(r);
        }

        // PUT: api/Roles/5
        [HttpDelete]
        public bool DeleteRole(Role r)
        {
            return roleRepo.Delete(r);
        }

        // POST: api/Roles
        [HttpPost]
        public Role PostRole(Role r)
        {
            return roleRepo.Create(r);
        }
    }
}
