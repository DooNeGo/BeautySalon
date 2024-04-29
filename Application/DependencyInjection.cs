using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddMediator();
    }
}