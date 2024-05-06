using BeautySalon.Application.Interfaces;

namespace BeautySalon.UI;

public sealed partial class App
{
    private readonly IIdentityService _identityService;
    private readonly IServiceProvider _serviceProvider;

    public App(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _identityService = identityService;
        _serviceProvider = serviceProvider;
        
        MainPage = new AppShell();
        UserAppTheme = AppTheme.Light;
    }
}