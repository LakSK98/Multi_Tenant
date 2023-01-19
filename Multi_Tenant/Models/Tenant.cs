using System;
using System.Collections.Generic;

namespace Multi_Tenant.Models
{
    public partial class Tenant
    {
        public int TenantId { get; set; }
        public string? TenantName { get; set; }
        public int? ClientId { get; set; }

        public virtual Client? Client { get; set; }
    }
}
