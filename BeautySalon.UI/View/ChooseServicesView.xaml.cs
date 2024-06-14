using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ChooseServicesView
{
    private readonly ChooseServicesViewModel _viewModel;

    public ChooseServicesView(ChooseServicesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}