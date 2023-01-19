using Microsoft.EntityFrameworkCore;
using Multi_Tenant.Data;

namespace Multi_Tenant.ContextFactory
{
    public class DbConnectionStringFactory : IDbConnectionStringFactory
    {
        private readonly SystemDbContext systemDb;
        public DbConnectionStringFactory(SystemDbContext systemDb) {
            this.systemDb = systemDb;
        }
        public async Task<string> GetConnectionString(int tenantId)
        {
            var tenant = await systemDb.Tenants.FindAsync(tenantId);
            return $"Server={tenant.Client.DatabaseServer};Database={tenant.Client.DatabaseName}b;Trusted_Connection=True;";
        }

        public async Task<Dictionary<string,string>> GetConnectionStrings()
        {
            Dictionary<string,string> conns = new Dictionary<string,string>();
            var consList = systemDb.Tenants.Select(t => new { id = t.TenantId, conStr = $"Server={t.Client.DatabaseServer};Database={t.Client.DatabaseName};Trusted_Connection=True;" }).ToListAsync().Result;
            foreach(var cons in consList )
            {
                conns.Add(Convert.ToString(cons.id), cons.conStr);
            }
            return conns;
        }
    }
}
