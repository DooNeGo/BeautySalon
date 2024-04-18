using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignIn;

public sealed partial class LoginView : ContentPage, IDisposable
{
    private readonly Animation _shadeBorderAnimation;
    private readonly Animation _unshadeBorderAnimation;

    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
        _shadeBorderAnimation = new Animation(a => BorderShadow.Opacity = (float)a, 0, 0.35, Easing.Linear);
        _unshadeBorderAnimation = new Animation(a => BorderShadow.Opacity = (float)a, 0.35, 0, Easing.Linear);
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(400);
        UsernameEntry.Focus();
    }

    private void Entry_Focused(object sender, FocusEventArgs e)
    {
        if (!_shadeBorderAnimation.IsEnabled && BorderShadow.Opacity is 0f)
        {
            LoginBorder.AbortAnimation(nameof(_unshadeBorderAnimation));
            LoginBorder.ScaleTo(1.015);
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

        if (!_unshadeBorderAnimation.IsEnabled && BorderShadow.Opacity is not 0f)
        {
            LoginBorder.AbortAnimation(nameof(_shadeBorderAnimation));
            LoginBorder.ScaleTo(1);
            _unshadeBorderAnimation.Commit(this, nameof(_unshadeBorderAnimation));
        }
    }

    public void Dispose()
    {
        _shadeBorderAnimation.Dispose();
        _unshadeBorderAnimation.Dispose();
        GC.SuppressFinalize(this);
    }
}