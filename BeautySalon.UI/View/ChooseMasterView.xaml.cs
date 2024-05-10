using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class ChooseMasterView
{
    public ChooseMasterView(ChooseMasterViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}