using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class MainView
{
    public MainView(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}