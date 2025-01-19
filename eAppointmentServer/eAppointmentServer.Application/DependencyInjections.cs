using Microsoft.Extensions.DependencyInjection;

namespace eAppointmentServer.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(typeof(DependencyInjections).Assembly);
            });

            return services;
        }
    }
}