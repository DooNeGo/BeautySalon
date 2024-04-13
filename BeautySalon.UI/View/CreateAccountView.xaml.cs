using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class CreateAccountView : ContentPage
{
	public CreateAccountView(CreateAccountViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await Task.Delay(400);
        FirstNameEntry.Focus();
    }
}