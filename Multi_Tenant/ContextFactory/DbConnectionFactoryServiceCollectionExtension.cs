using Microsoft.AspNetCore.Connections;

namespace Multi_Tenant.ContextFactory
{
    public static class DbConnectionFactoryServiceCollectionExtensions
    {
        public static IServiceCollection AddDbConnectionFactory(this IServiceCollection collection, Dictionary<string, string> connections)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));

            return collection.AddSingleton<IDbConnectionFactory, DbConnectionFactory>(factory => new DbConnectionFactory(connections));
        }
    }
}
