using System.Diagnostics;
using BeautySalon.Application.Commands.AddAppointment;
using BeautySalon.Application.Commands.UpdateAppointment;
using BeautySalon.Domain;
using BeautySalon.UI.Messages;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public enum Action
{
    Delete,
    Update,
    Add
}

public sealed partial class ConfirmAppointmentViewModel(IMediator mediator, IMessenger messenger) : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private Appointment? _appointment;

    private Action _action;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Appointment = (Appointment)query["Appointment"];
        _action = (Action)query["Action"];
    }
    
    [RelayCommand]
    private async Task ConfirmAppointment(CancellationToken cancellationToken = default)
    {
        try
        {
            await ExecuteAppointmentActionAsync(cancellationToken).ConfigureAwait(false);
            await GoBackAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e);
            await ShowErrorAlertAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private Task ExecuteAppointmentActionAsync(CancellationToken cancellationToken) => _action switch
    {
        Action.Add => Task.Run(async () =>
        {
            await AddAppointmentAsync(cancellationToken).ConfigureAwait(false);
            await ShowSuccessfulAddAlertAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken),
        
        Action.Update => Task.Run(async () =>
        {
            await UpdateAppointmentAsync(cancellationToken).ConfigureAwait(false);
            await ShowSuccessfulUpdateAlertAsync(cancellationToken).ConfigureAwait(false);
        }, cancellationToken),
        
        _ => throw new ArgumentOutOfRangeException($"There is no implementation for {_action}")
    };

    private async Task UpdateAppointmentAsync(CancellationToken cancellationToken)
    {        
        Guard.IsNotNull(Appointment, nameof(Appointment));
        await mediator
            .Send(new UpdateAppointmentCommand(Appointment), cancellationToken)
            .ConfigureAwait(false);
        messenger.Send(new AppointmentUpdatedMessage(Appointment.Id));
    }

    private async Task AddAppointmentAsync(CancellationToken cancellationToken)
    {
        Guard.IsNotNull(Appointment, nameof(Appointment));
        Guid id = await mediator
            .Send(new AddAppointmentCommand(Appointment), cancellationToken)
            .ConfigureAwait(false);
        messenger.Send(new AppointmentAddedMessage(id));
    }

    private static Task GoBackAsync(CancellationToken token) =>
        Microsoft.Maui.Controls.Application.Current!.Dispatcher.DispatchAsync(() =>
            Shell.Current.GoToAsync("../../..", true)
                .WaitAsync(token)).WaitAsync(token);

    private static Task ShowSuccessfulUpdateAlertAsync(CancellationToken token) =>
        ShowAlertAsync("Подтверждение", "Вы успешно обновили запись", "Ок", token);
    
    private static Task ShowSuccessfulAddAlertAsync(CancellationToken token) =>
        ShowAlertAsync("Подтверждение", "Вы успешно записались", "Ок", token);
    
    private static Task ShowErrorAlertAsync(CancellationToken token) =>
        ShowAlertAsync("Подтверждение", "Ошибка", "Ок", token);

    private static Task ShowAlertAsync(string title, string message, string cancel,
        CancellationToken token = default) => App.Current!.Dispatcher
        .DispatchAsync(() => App.Current.MainPage!
            .DisplayAlert(title, message, cancel)
            .WaitAsync(token)).WaitAsync(token);
}