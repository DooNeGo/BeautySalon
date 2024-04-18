using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignUp;

public sealed partial class CreateUserView : ContentPage
{
	public CreateUserView(CreateUserViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(400);
        UsernameEntry.Focus();
    }
}