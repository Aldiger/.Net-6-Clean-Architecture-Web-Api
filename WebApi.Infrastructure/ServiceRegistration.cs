using WebApi.Core.Repositories;
using WebApi.Domain.Interfaces;
using WebApi.Infrastructure.Context;
using WebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace WebApi.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDbContext<WebApiContext>(x =>
        {
            x.UseSqlServer(configuration.GetConnectionString("Default"));
            x.EnableSensitiveDataLogging();
        });
        services.TryAddScoped<DbContext, WebApiContext>();


        // add hardcoded test user to db on startup
        using (var serviceScope = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            Console.WriteLine("Service scope created");
            var context = serviceScope.ServiceProvider.GetRequiredService<WebApiContext>();
            Console.WriteLine("Context received");
            context.Database.Migrate();
            Console.WriteLine("migration succeed");
        }


        services.TryAddScoped(typeof(IUnitOfWork), typeof(UnitOfWork<DbContext>));

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}