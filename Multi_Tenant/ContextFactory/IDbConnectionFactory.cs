using System.Data;

namespace Multi_Tenant.ContextFactory
{
    public interface IDbConnectionFactory
    {
        string GetConnectionString(string connectionName);
        IDbConnection CreateDbConnection(string connectionString);
    }
}
