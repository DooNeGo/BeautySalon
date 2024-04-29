using BeautySalon.UI.View.SignUp;

namespace BeautySalon.UI.View.SignIn;

public sealed partial class StartView
{
    private readonly IServiceProvider _serviceProvider;

    public StartView(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _serviceProvider = serviceProvider;
    }

    private void LoginWithAccountButton_Clicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(_serviceProvider.GetRequiredService<LoginView>());
    }

    private void CreateAccountButton_Clicked(object? sender, EventArgs e)
    {
        Navigation.PushAsync(_serviceProvider.GetRequiredService<CreateUserView>());
    }
}