using BeautySalon.Application.Commands.AddAppointment;
using BeautySalon.Domain;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

[QueryProperty(nameof(Appointment), "Appointment")]
public sealed partial class ConfirmAppointmentViewModel(IMediator mediator) : ObservableObject
{
    [ObservableProperty] private Appointment? _appointment;

    [RelayCommand]
    private async Task ConfirmAppointment(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(Appointment, nameof(Appointment));
        try
        {
            await mediator.Send(new AddAppointmentCommand(Appointment), cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            //await ShowErrorAlertAsync(cancellationToken).ConfigureAwait(false);
        }
        
        await ShowSuccessfulAlertAsync(cancellationToken).ConfigureAwait(false);
        await GoBackAsync(cancellationToken).ConfigureAwait(false);
    }

    private static Task GoBackAsync(CancellationToken token) =>
        Shell.Current.GoToAsync("../../..", true).WaitAsync(token);

    private static Task ShowSuccessfulAlertAsync(CancellationToken token) =>
        ShowAlertAsync("Подтверждение", "Вы успешно записались", "Ок", token);
    
    private static Task ShowErrorAlertAsync(CancellationToken token) =>
        ShowAlertAsync("Подтверждение", "Ошибка", "Ок", token);

    private static Task ShowAlertAsync(string title, string message, string cancel,
        CancellationToken token = default) =>
        App.Current!.Dispatcher.DispatchAsync(async () =>
                await App.Current.MainPage
                    .DisplayAlert(title, message, cancel)
                    .WaitAsync(token)
                    .ConfigureAwait(false))
            .WaitAsync(token);
}