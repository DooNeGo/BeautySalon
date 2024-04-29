using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignUp;

public sealed partial class CreateAccountView
{
    public CreateAccountView(CreateAccountViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        await Task.Delay(400);
        FirstNameEntry.Focus();

        base.OnAppearing();
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

        TryHideKeyboard();
    }

    private void TryHideKeyboard()
    {
        if (FirstNameEntry.IsSoftInputShowing()) FirstNameEntry.HideSoftInputAsync(CancellationToken.None);
    }
}