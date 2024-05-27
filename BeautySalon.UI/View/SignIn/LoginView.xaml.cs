using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignIn;

public sealed partial class LoginView
{
    private const double IncrementValue = 0.03;
    private readonly double _defaultLoginBorderScale;
    private readonly double _incrementedLoginBoarderScale;

    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        _defaultLoginBorderScale = LoginBorder.Scale;
        _incrementedLoginBoarderScale = _defaultLoginBorderScale + IncrementValue;
    }

    protected override async void OnAppearing()
    {
        await Task.Delay(400);
        UsernameEntry.Focus();

        base.OnAppearing();
    }

    private async void Entry_OnFocused(object? sender, FocusEventArgs e) =>
        await LoginBorder.ScaleTo(_incrementedLoginBoarderScale, 400, Easing.Default).ConfigureAwait(false);

    private async void Entry_OnUnfocused(object? sender, FocusEventArgs e) =>
        await LoginBorder.ScaleTo(_defaultLoginBorderScale, easing: Easing.Linear).ConfigureAwait(false);

    private async void TapGestureRecognizer_OnTapped(object? sender, TappedEventArgs e)
    {
        UsernameEntry.Unfocus();
        PasswordEntry.Unfocus();

        await UsernameEntry.HideSoftInputAsync(CancellationToken.None).ConfigureAwait(false);
    }

    private async void PasswordEntry_OnCompleted(object? sender, EventArgs e)
    {
        await UsernameEntry.HideSoftInputAsync(CancellationToken.None).ConfigureAwait(false);
        LoginButton.SendClicked();
    }
}