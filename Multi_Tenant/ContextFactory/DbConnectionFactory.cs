using System.Data.Common;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Multi_Tenant.Data;

namespace Multi_Tenant.ContextFactory
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly IDictionary<string, string> _connectionDictionary;
        private readonly IHttpContextAccessor httpContext = new HttpContextAccessor();

        public DbConnectionFactory(IDictionary<string, string> connectionDictionary)
        {
            _connectionDictionary = connectionDictionary;
        }

        public string GetConnectionString(string connectionName)
        {
            _connectionDictionary.TryGetValue(connectionName, out string connectionString);

            if (connectionString == null)
            {
                throw new Exception(string.Format("Connection string {0} was not found", connectionName));
            }

            return connectionString;
        }

        public IDbConnection CreateDbConnection(string connectionString)
        {
            // Assume failure.
            DbConnection connection = null;

            // Create the DbProviderFactory and DbConnection.
            if (connectionString != null)
            {
                DbProviderFactory factory = SqlClientFactory.Instance;
                connection = factory.CreateConnection();
                connection.ConnectionString = connectionString;
            }
            // Return the connection.
            return connection;
        }

        public ClientDbContext GetDbContext()
        {
            string tenantId = httpContext.HttpContext.Request.Headers["tenantId"].First();
            string connectionString = GetConnectionString(tenantId);
            DbContextOptionsBuilder dbContextOptionsBuilder = new DbContextOptionsBuilder();
            dbContextOptionsBuilder.UseSqlServer(connectionString);
            return new ClientDbContext(dbContextOptionsBuilder.Options);
        }
    }

}
