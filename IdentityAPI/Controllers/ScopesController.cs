using IdentityAPI.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityAPI.Controllers
{
    public class ScopesController : ApiController
    {
        ScopeRepository scopeRepo = new ScopeRepository();

        // GET: api/Scopes
        [HttpGet]
        public IQueryable<Scope> GetScopes()
        {
            return scopeRepo.GetAll();
        }

        //GET: api/Scopes/5
        [HttpGet]
        public Scope GetScope(int id)
        {
            return scopeRepo.Get(id);
        }

        // PUT: api/Scopes/{scope}
        [HttpPut]
        public Scope PutScope(Scope s)
        {
            return scopeRepo.Update(s);
        }

        // PUT: api/Scopes/5
        [HttpDelete]
        public bool DeleteScope(Scope s)
        {
            return scopeRepo.Delete(s);
        }

        // POST: api/Scopes
        [HttpPost]
        public Scope PostScope(Scope s)
        {
            return scopeRepo.Create(s);
        }
    }
}
