using BeautySalon.Application.Interfaces;
using BeautySalon.UI.View.SignIn;

namespace BeautySalon.UI;

public sealed partial class App
{
    private readonly IIdentityService _identityService;
    private readonly IServiceProvider _serviceProvider;

    public App(IIdentityService identityService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = new AppShell();
        UserAppTheme = AppTheme.Light;

        _identityService = identityService;
        _serviceProvider = serviceProvider;

        _identityService.LoginSuccessful += _ => MainPage.Navigation.PopToRootAsync();
    }

    protected override void OnStart()
    {
        if (_identityService.CurrentUser is null)
        {
            MainPage!.Navigation.PushAsync(_serviceProvider.GetRequiredService<StartView>());
        }
    }
}