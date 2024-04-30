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
        FirstNameEntry.Unfocus();
        LastNameEntry.Unfocus();
        MiddleNameEntry.Unfocus();
        PhoneEntry.Unfocus();

        FirstNameEntry.HideSoftInputAsync(CancellationToken.None);
    }

    private async void PhoneEntry_Completed(object sender, EventArgs e)
    { 
        await FirstNameEntry.HideSoftInputAsync(CancellationToken.None);
        FinishButton.SendClicked();
    }
}