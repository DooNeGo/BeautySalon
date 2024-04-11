﻿using BeautySalon.Application;
using BeautySalon.View;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BeautySalon.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services
               .AddMediator()
               .AddDbContext<IApplicationContext, ApplicationContext>()
               .AddSingleton<LoginPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
