using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class LoginView : ContentPage
{
    private CancellationTokenSource _cancellationToken = new();

    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Login button", "Button was clicked", "Close");
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();
        _cancellationToken = new CancellationTokenSource();

        ShadeEntryBorder(_cancellationToken.Token);
    }

    private void ContentPage_Focused(object sender, FocusEventArgs e)
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();
        _cancellationToken = new CancellationTokenSource();

        UsernameEntry.Unfocus();
        PasswordEntry.Unfocus();

        UnshadeEntryBorder(_cancellationToken.Token);
    }

    private async Task ShadeEntryBorder(CancellationToken cancellationToken)
    {
        while (BorderShadow.Opacity < 0.4 && !cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(25), cancellationToken);
            BorderShadow.Opacity += 0.05f;
        }
    }

    private async Task UnshadeEntryBorder(CancellationToken cancellationToken)
    {
        while (BorderShadow.Opacity > 0 && !cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(25), cancellationToken);
            BorderShadow.Opacity -= 0.05f;
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        _cancellationToken.Cancel();
        _cancellationToken.Dispose();
        _cancellationToken = new CancellationTokenSource();

        UsernameEntry.Unfocus();
        PasswordEntry.Unfocus();

        if (UsernameEntry.IsSoftInputShowing() || PasswordEntry.IsSoftInputShowing())
        {
            UsernameEntry.HideSoftInputAsync(CancellationToken.None);
            PasswordEntry.HideSoftInputAsync(CancellationToken.None);
        }

        UnshadeEntryBorder(_cancellationToken.Token);
    }
}