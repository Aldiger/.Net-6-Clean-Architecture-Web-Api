using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WebApi.Application.Abstract;
using WebApi.Application.Concrete;
using WebApi.Application.Dto.Product.Request;
using WebApi.Application.Validation;
using WebApi.Infrastructure;

namespace WebApi.Application
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

           
            services.AddScoped<IProductManager, ProductManager>();

            services.AddInfrastructure(configuration);


            return services;
        }
    }
}