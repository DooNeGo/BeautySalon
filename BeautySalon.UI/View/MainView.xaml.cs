using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class MainView
{
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void MastersViewAll_OnTapped(object? sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(MastersViewModel));
    }
    
    private void ServicesViewAll_OnTapped(object? sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ServicesViewModel));
    }

    private void SignUpForServiceButton_OnClicked(object? sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(SignUpForServicePageModel));
    }
}