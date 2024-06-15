using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ChooseServicesView
{
    public ChooseServicesView(ChooseServicesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}