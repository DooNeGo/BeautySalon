using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class MastersView
{
    public MastersView(MastersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}