using System;
using System.Collections.Generic;

namespace Multi_Tenant.Models
{
    public partial class Client
    {
        public Client()
        {
            Tenants = new HashSet<Tenant>();
        }

        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public string? DatabaseServer { get; set; }
        public string? DatabaseName { get; set; }

        public virtual ICollection<Tenant> Tenants { get; set; }
    }
}
