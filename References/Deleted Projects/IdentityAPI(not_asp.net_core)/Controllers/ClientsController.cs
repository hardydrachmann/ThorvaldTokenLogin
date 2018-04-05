using IdentityAPI.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IdentityAPI.Controllers
{
    public class ClientsController : ApiController
    {
        ClientRepository clientRepo = new ClientRepository();

        // GET: api/Clients
        [HttpGet]
        public IQueryable<Client> GetClients()
        {
            return clientRepo.GetAll();
        }

        //GET: api/Clients/5
        [HttpGet]
        public Client GetClient(int id)
        {
            return clientRepo.Get(id);
        }

        // PUT: api/Clients/{client}
        [HttpPut]
        public Client PutClient(Client c)
        {
            return clientRepo.Update(c);
        }

        // PUT: api/Clients/5
        [HttpDelete]
        public bool DeleteClient(Client c)
        {
            return clientRepo.Delete(c);
        }

        // POST: api/Clients
        [HttpPost]
        public Client PostClient(Client c)
        {
            return clientRepo.Create(c);
        }
    }
}
