using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

[QueryProperty(nameof(Appointment), "Appointment")]
public sealed partial class ConfirmAppointmentViewModel : ObservableObject
{
    [ObservableProperty] private Appointment _appointment = null!;
}