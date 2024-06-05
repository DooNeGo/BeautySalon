using AsyncAwaitBestPractices;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class AppointmentsViewModel : ObservableObject
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;

    [ObservableProperty] private List<Appointment> _appointments = [];
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
    private async Task RefreshAsync()
    {
        CheckCurrentState();

        if (_globalContext.Customer is not null)
        {
            Appointments = await _mediator
                .Send(new GetAppointmentsByCustomerIdQuery(_globalContext.Customer.Id))
                .ConfigureAwait(false);
        }

        IsRefreshing = false;
    }

    [RelayCommand]
    private Task GoToAuthorizationAsync() => Shell.Current.GoToAsync(nameof(StartViewModel));

    private void CheckCurrentState() =>
        CurrentState = _globalContext.Customer is null ? States.NotLoggedIn
            : States.Normal;

    private static class States
    {
        public const string Normal = nameof(Normal);
        public const string NotLoggedIn = nameof(NotLoggedIn);
    }
}
