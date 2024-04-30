using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ServicesView
{
    public ServicesView(ServicesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}