using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class AppointmentsView
{
    public AppointmentsView(AppointmentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}