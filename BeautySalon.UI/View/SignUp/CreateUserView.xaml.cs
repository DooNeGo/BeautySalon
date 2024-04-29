using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignUp;

public sealed partial class CreateUserView
{
    public CreateUserView(CreateUserViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        await Task.Delay(400);
        UsernameEntry.Focus();

        base.OnAppearing();
    }
}