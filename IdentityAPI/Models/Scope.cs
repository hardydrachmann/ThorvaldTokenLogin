﻿using System;
using System.Collections.Generic;

namespace IdentityAPI.Models
{
    public partial class Scope
    {
        public Scope()
        {
            ClientScope = new HashSet<ClientScope>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ClientScope> ClientScope { get; set; }
    }
}
