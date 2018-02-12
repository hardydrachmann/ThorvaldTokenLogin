using System;
using System.Collections.Generic;

namespace IdentityAPI.Models
{
    public partial class Client
    {
        public Client()
        {
            ClientScope = new HashSet<ClientScope>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string LoginUri { get; set; }
        public string LogoutUri { get; set; }

        public ICollection<ClientScope> ClientScope { get; set; }
    }
}
