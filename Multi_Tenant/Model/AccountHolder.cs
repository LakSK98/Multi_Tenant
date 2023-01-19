using System;
using System.Collections.Generic;

namespace Multi_Tenant.Model
{
    public partial class AccountHolder
    {
        public AccountHolder()
        {
            AccountDetails = new HashSet<AccountDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<AccountDetail> AccountDetails { get; set; }
    }
}
