using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class StartView : ContentPage
{
	public StartView()
	{
		InitializeComponent();
	}

    private void LoginWithAccountButton_Clicked(object sender, EventArgs e)
    {
		Shell.Current.GoToAsync(nameof(LoginViewModel));
    }

    private void CreateAccountButton_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(CreateAccountViewModel));
    }
}