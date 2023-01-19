using System;
using System.Collections.Generic;

namespace Multi_Tenant.Model
{
    public partial class AccountDetail
    {
        public string AccountNo { get; set; } = null!;
        public float? Balance { get; set; }
        public string? AccountType { get; set; }
        public int? AccountHolderId { get; set; }

        public virtual AccountHolder? AccountHolder { get; set; }
    }
}
