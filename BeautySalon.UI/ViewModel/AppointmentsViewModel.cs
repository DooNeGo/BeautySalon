using System.Collections.ObjectModel;
using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using BeautySalon.UI.Messages;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class AppointmentsViewModel
    : ObservableObject, IRecipient<AppointmentAddedMessage>, IRecipient<AppointmentUpdatedMessage>
{
    private readonly IMediator _mediator;
    private readonly IIdentityService _identityService;

    [ObservableProperty] private ObservableCollection<Appointment> _appointments = [];
    [ObservableProperty] private string _currentState = States.NotLoggedIn;
    [ObservableProperty] private bool _isRefreshing;

    public AppointmentsViewModel(IMediator mediator, IIdentityService identityService, IMessenger messenger)
    {
        (_mediator, _identityService) = (mediator, identityService);
        
        messenger.Register<AppointmentAddedMessage>(this);
        messenger.Register<AppointmentUpdatedMessage>(this);
        
        identityService.Authorized += _ =>
        {
            UpdateCurrentState();
            IsRefreshing = true;
        };
        identityService.Unauthorized += UpdateCurrentState;
        UpdateCurrentState();
    }

    public void Receive(AppointmentAddedMessage message) => IsRefreshing = true;

    public void Receive(AppointmentUpdatedMessage message) => IsRefreshing = true;

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        Guard.IsNotNull(_identityService.CurrentUser, nameof(_identityService.CurrentUser));

        await Task.Delay(300, cancellationToken).ConfigureAwait(false);
        List<Appointment> response = await _mediator
            .Send(new GetAppointmentsByCustomerIdQuery(_identityService.CurrentUser.Customer!.Id), cancellationToken)
            .ConfigureAwait(false);

        await Task.Run(response.Sort, cancellationToken).ConfigureAwait(false);
        Appointments = [.. response];
        IsRefreshing = false;
    }

    [RelayCommand]
    private Task GoToAuthorizationAsync(CancellationToken cancellationToken) =>
        Shell.Current.GoToAsync(nameof(StartViewModel)).WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task DeleteAppointment(Appointment appointment)
    {
        if (await Microsoft.Maui.Controls.Application.Current!.MainPage!.DisplayAlert(
                    "Потвеждение удаления", "Вы точно хотите удалить запись?", "Да", "Нет")
                .ConfigureAwait(false))
            Appointments.Remove(appointment);
    }

    [RelayCommand]
    private Task GoToEditPageAsync(Appointment appointment, CancellationToken cancellationToken = default) =>
        Shell.Current.GoToAsync(nameof(ChooseServicesViewModel), new Dictionary<string, object>
        {
            { "Appointment", appointment }
        }).WaitAsync(cancellationToken);

    private void UpdateCurrentState() =>
        CurrentState = _identityService.CurrentUser is null
            ? States.NotLoggedIn
            : States.Normal;

    private static class States
    {
        public const string Normal = nameof(Normal);
        public const string NotLoggedIn = nameof(NotLoggedIn);
    }
}