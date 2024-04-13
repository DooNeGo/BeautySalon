using BeautySalon.UI.ViewModel;
using CommunityToolkit.Maui.Core.Platform;

namespace BeautySalon.UI.View;

public partial class LoginView : ContentPage
{
    //private CancellationTokenSource _cancellationToken = new();
    private readonly Animation _shadeBorderAnimation;
    private readonly Animation _unshadeBorderAnimation;

    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        _shadeBorderAnimation = new Animation(a => BorderShadow.Opacity = (float)a, 0, 0.35f, Easing.Linear);
        _unshadeBorderAnimation = new Animation(a => BorderShadow.Opacity = (float)a, 0.35f, 0, Easing.Linear);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(400);
        UsernameEntry.Focus();
    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        DisplayAlert("Login button", "Button was clicked", "Close");
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (!_shadeBorderAnimation.IsEnabled && BorderShadow.Opacity is 0f)
        {
            LoginButton.Focus();
            LoginBorder.ScaleTo(1.01f);
            LoginBorder.AbortAnimation(nameof(_unshadeBorderAnimation));
            _shadeBorderAnimation.Commit(this, nameof(_shadeBorderAnimation));
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        UsernameEntry.Unfocus();
        PasswordEntry.Unfocus();

        if (UsernameEntry.IsSoftInputShowing())
        {
            UsernameEntry.HideSoftInputAsync(CancellationToken.None);
        }

        if (PasswordEntry.IsSoftInputShowing())
        {
            PasswordEntry.HideSoftInputAsync(CancellationToken.None);
        }

        if (!_unshadeBorderAnimation.IsEnabled && BorderShadow.Opacity is not 0f)
        {
            LoginBorder.ScaleTo(1f);
            LoginBorder.AbortAnimation(nameof(_shadeBorderAnimation));
            _unshadeBorderAnimation.Commit(this, nameof(_unshadeBorderAnimation));
        }
    }
}