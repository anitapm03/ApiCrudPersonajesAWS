using Amazon.Lambda.Annotations;
using ApiCrudPersonajesAWS.Data;
using ApiCrudPersonajesAWS.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCrudPersonajesAWS;

[LambdaStartup]
public class Startup
{

    public void ConfigureServices(IServiceCollection services)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true);

        var configuration = builder.Build();
        services.AddSingleton<IConfiguration>(configuration);

        services.AddTransient<RepositoryPersonajes>();
        string connectionString =
            configuration.GetConnectionString("MySql");
        services.AddDbContext<PersonajesContext>
            (options => options.UseMySql(connectionString
            , ServerVersion.AutoDetect(connectionString)));
    }

}
