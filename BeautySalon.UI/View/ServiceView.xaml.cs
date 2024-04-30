using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ServiceView
{
    public ServiceView(ServiceViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}