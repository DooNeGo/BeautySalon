using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using BeautySalon.UI.Shells;

namespace BeautySalon.UI;

public sealed partial class App
{
    public App(IIdentityService identityService)
    {
        InitializeComponent();

        identityService.LoginSuccesful += IdentityService_LoginSuccesful;
        MainPage = new LoginShell();
        UserAppTheme = AppTheme.Light;
    }

    private void IdentityService_LoginSuccesful(User obj)
    {
        MainPage = new MainShell();
    }
}