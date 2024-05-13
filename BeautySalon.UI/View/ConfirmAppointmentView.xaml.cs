using BeautySalon.UI.ViewModel;

namespace BeautySalon.UI.View;

public sealed partial class ConfirmAppointmentView
{
    public ConfirmAppointmentView(ConfirmAppointmentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}