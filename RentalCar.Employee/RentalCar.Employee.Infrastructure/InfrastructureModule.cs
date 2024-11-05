using Microsoft.Extensions.DependencyInjection;
using RentalCar.Employees.Core.Repositories;
using RentalCar.Employees.Core.Services;
using RentalCar.Employees.Infrastructure.MessageBus;
using RentalCar.Employees.Infrastructure.Repositories;
using RentalCar.Employees.Infrastructure.Services;

namespace RentalCar.Employees.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) 
        {
            services
                .AddServices();
            return services;
        }

        private static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddSingleton<ILoggerService, LoggerService>();
            services.AddSingleton<IRabbitMqService, RabbitMqService>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            return services;
        }

    }
}
