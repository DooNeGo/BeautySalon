using BeautySalon.Application;
using BeautySalon.Infrastructure;
using BeautySalon.UI.View;
using BeautySalon.UI.ViewModel;
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
                fonts.AddFont("Lato-Regular.ttf", "Lato");
                fonts.AddFont("Floriselscript.ttf", "Florisel");
            });

        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddTransientWithShellRoute<LoginView, LoginViewModel>(nameof(LoginViewModel))
            .AddTransientWithShellRoute<CreateAccountView, CreateAccountViewModel>(nameof(CreateAccountViewModel));

#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}