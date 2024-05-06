using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignIn;

public sealed partial class StartView
{
    public StartView()
    {
        InitializeComponent();
    }

    private void LoginWithAccountButton_Clicked(object? sender, EventArgs e) =>
            Shell.Current.GoToAsync(nameof(LoginViewModel));

    private void CreateAccountButton_Clicked(object? sender, EventArgs e) =>
            Shell.Current.GoToAsync(nameof(CreateUserViewModel));
}