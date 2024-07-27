using Application.Services;
using Contracts;
using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure.Glue
{
    public static class DIExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EmployeeService>();
            services.AddScoped(typeof(Irepository<>), typeof(Repository<>));

            return services;
        }
    }
}
