using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class MastersView : ContentPage
{
    public MastersView(MastersViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}