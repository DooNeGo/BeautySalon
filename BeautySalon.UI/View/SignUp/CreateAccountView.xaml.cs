using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignUp;

public sealed partial class CreateAccountView : ContentPage
{
    private readonly CreateAccountViewModel _viewModel;

    public CreateAccountView(CreateAccountViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(400);
        FirstNameEntry.Focus();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        UnfocusEntries();
    }

    private void PhoneEntry_Completed(object sender, EventArgs e)
    {
        UnfocusEntries();
    }

    private void UnfocusEntries()
    {
        FirstNameEntry.Unfocus();
        LastNameEntry.Unfocus();
        MiddleNameEntry.Unfocus();
        PhoneEntry.Unfocus();

        TryHideKeyaboard();
    }

    private void TryHideKeyaboard()
    {
        if (FirstNameEntry.IsSoftInputShowing())
        {
            FirstNameEntry.HideSoftInputAsync(CancellationToken.None);
        }
    }
}