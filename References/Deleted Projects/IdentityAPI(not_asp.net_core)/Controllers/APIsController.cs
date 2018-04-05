using IdentityAPI.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityAPI.Controllers
{
    public class APIsController : ApiController
    {
        APIRepository apiRepo = new APIRepository();

        // GET: api/APIs
        [HttpGet]
        public IQueryable<API> GetAPIs()
        {
            return apiRepo.GetAll();
        }

        //GET: api/APIs/5
        [HttpGet]
        public API GetAPI(int id)
        {
            return apiRepo.Get(id);
        }

        // PUT: api/APIs/{api}
        [HttpPut]
        public API PutAPI(API a)
        {
            return apiRepo.Update(a);
        }

        // PUT: api/APIs/5
        [HttpDelete]
        public bool DeleteAPI(API a)
        {
            return apiRepo.Delete(a);
        }

        // POST: api/APIs
        [HttpPost]
        public API PostAPI(API a)
        {
            return apiRepo.Create(a);
        }
    }
}
