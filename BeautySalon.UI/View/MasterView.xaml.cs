using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class MasterView
{
    public MasterView(MasterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}