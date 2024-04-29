using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public partial class ServicesView : ContentPage
{
    public ServicesView(ServicesViewModel viewModel)
    {
        InitializeComponent();
        
        BindingContext = viewModel;
    }
}