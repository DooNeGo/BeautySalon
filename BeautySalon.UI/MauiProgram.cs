using BeautySalon.Application;
using BeautySalon.Infrastructure;
using BeautySalon.UI.Services;
using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Logging;

namespace BeautySalon.UI;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
        {
            fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            fonts.AddFont("Lato-Regular.ttf", "Lato");
            fonts.AddFont("Floriselscript.ttf", "Florisel");
            fonts.AddFont("FASolid.otf", "FASolid");
        });

        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddUI()
            .AddSingleton<IMessenger>(WeakReferenceMessenger.Default)
            .AddSingleton<IMasterScheduleService, MasterScheduleService>()
            .AddSingleton<GlobalContext>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        AppDomain.CurrentDomain.UnhandledException += (_, args) =>
            Clipboard.SetTextAsync(args.ExceptionObject.ToString());

        return builder.Build();
    }
}