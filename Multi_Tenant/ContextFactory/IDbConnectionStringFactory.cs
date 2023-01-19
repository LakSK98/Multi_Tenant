namespace Multi_Tenant.ContextFactory
{
    public interface IDbConnectionStringFactory
    {
        Task<string> GetConnectionString(int tenantId);
        Task<Dictionary<string,string>> GetConnectionStrings();
    }
}
