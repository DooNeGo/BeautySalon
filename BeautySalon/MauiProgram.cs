using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BeautySalon;

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

        builder.Configuration
               .AddJsonFile("appsettings.json", false, true);

        builder.Services
               .AddMediator()
               .AddDbContext<BeautySalonDBContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
