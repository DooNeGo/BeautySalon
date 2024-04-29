using BeautySalon.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddDbContext<IApplicationContext, ApplicationContext>()
                .AddSingleton<IIdentityService, IdentityService>();
    }
}