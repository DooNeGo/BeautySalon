using AsyncAwaitBestPractices;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.ObjectModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class AppointmentsViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;

    [ObservableProperty] private ObservableCollection<Appointment> _appointments = [];
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private string _currentState = States.NotLoggedIn;

    public AppointmentsViewModel(IMediator mediator, GlobalContext globalContext)
    {
        _mediator = mediator;
        _globalContext = globalContext;

        _globalContext.PropertyChanged += async (_, e) =>
        {
            if (e.PropertyName is not nameof(_globalContext.Customer)) return;
            await RefreshAsync().ConfigureAwait(false);
        };

        RefreshAsync().SafeFireAndForget();
    }

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        UpdateCurrentState();

        if (_globalContext.Customer is not null)
        {
            List<Appointment> response = await _mediator
                .Send(new GetAppointmentsByCustomerIdQuery(_globalContext.Customer.Id), cancellationToken)
                .ConfigureAwait(false);

            response.Sort((appointment, appointment1) =>
                (int)(appointment.DateTime - appointment1.DateTime).TotalDays);

            Appointments = [.. response];
        }

        IsRefreshing = false;
    }

    [RelayCommand]
    private Task GoToAuthorizationAsync(CancellationToken cancellationToken) =>
        Shell.Current.GoToAsync(nameof(StartViewModel)).WaitAsync(cancellationToken);

    [RelayCommand]
    private async Task DeleteAppointment(Appointment appointment)
    {
        if (await App.Current!.MainPage!.DisplayAlert(
            "Потвеждение удаления", "Вы точно хотите удалить запись?", "Да", "Нет")
            .ConfigureAwait(false))
        {
            Appointments.Remove(appointment);
        }
    }

    private void UpdateCurrentState() =>
        CurrentState = _globalContext.Customer is null
            ? States.NotLoggedIn : States.Normal;

    private static class States
    {
        public const string Normal = nameof(Normal);
        public const string NotLoggedIn = nameof(NotLoggedIn);
    }
}
