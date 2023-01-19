using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Multi_Tenant.ContextFactory;
using Multi_Tenant.Data;
using Multi_Tenant.Model;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SystemDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<IDbConnectionStringFactory, DbConnectionStringFactory>();

/*builder.Services.AddDbContext<testDbContext>((serviceProvider, dbContextBuilder) =>
{
    var connectionStringPlaceHolder = builder.Configuration.GetConnectionString("PlaceHolderConnection");
    var httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
    int tenantId = int.Parse(httpContextAccessor.HttpContext.Request.Headers["tenantId"].First());
    var systemDb = serviceProvider.GetRequiredService<SystemDbContext>();
    var dbName = systemDb.Tenants.Find(tenantId).Client.DatabaseName;
    var connectionString = connectionStringPlaceHolder.Replace("{dbName}", dbName);
    dbContextBuilder.UseSqlServer(connectionString);
});*/

var connectionStringFactory = builder.Services.BuildServiceProvider().GetRequiredService<IDbConnectionStringFactory>();
//var httpContext = builder.Services.BuildServiceProvider().GetRequiredService<IHttpContextAccessor>();


//builder.Services.AddDbConnectionFactory(builder.Configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>());

builder.Services.AddDbConnectionFactory(connectionStringFactory.GetConnectionStrings().Result);

/*builder.Services.AddDbContext<ClientDbContext>((serviceProvider, options) =>
{
    var httpContext = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext;
    var tenantId = httpContext.Request.Headers["tenantId"].First();
    string conn = serviceProvider.GetRequiredService<IDbConnectionFactory>().GetConnectionString(tenantId);
    options.UseSqlServer(conn);
});*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
