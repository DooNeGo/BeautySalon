namespace BeautySalon.UI;

public sealed partial class App
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
        UserAppTheme = AppTheme.Light;
    }
}