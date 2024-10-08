﻿using BeautySalon.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services.AddDbContext<IApplicationContext, ApplicationContext>()
            .AddSingleton<IIdentityService, IdentityService>()
            .AddSingleton<IClock, Clock>();
}