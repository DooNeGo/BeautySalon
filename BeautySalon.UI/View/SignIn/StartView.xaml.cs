using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View.SignIn;

public sealed partial class StartView
{
    public StartView(StartViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}