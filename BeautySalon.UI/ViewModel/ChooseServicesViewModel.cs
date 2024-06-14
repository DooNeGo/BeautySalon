using System.Collections.Immutable;
using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.ComponentModel;
using AsyncAwaitBestPractices;
using BeautySalon.UI.Services;
using CommunityToolkit.Diagnostics;

namespace BeautySalon.UI.ViewModel;

public partial class SelectableObjectWrapper<T>(T value) : ObservableObject
{
    [ObservableProperty] private bool _isSelected;

    public T Value { get; init; } = value;
}

public sealed class SelectableService(Service service) : SelectableObjectWrapper<Service>(service);

public sealed partial class ChooseServicesViewModel : ObservableObject, IQueryAttributable
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    private readonly IMasterScheduleService _masterScheduleService;

    [ObservableProperty] private TimeOnly? _selectedTime;
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private ImmutableArray<SelectableService> _services = [];
    [ObservableProperty] private ImmutableArray<TimeOnly> _masterFreeTime = [];
    [ObservableProperty] private DateTime _minimumDateTime;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private bool _isErrorVisible;

    private Master? _master;
    private Appointment? _appointment;
    private Task _updateMasterFreeTimeTask = Task.CompletedTask;

    public ChooseServicesViewModel(IMediator mediator, IClock clock, GlobalContext globalContext,
        IMasterScheduleService masterScheduleService)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        _masterScheduleService = masterScheduleService;
        MinimumDateTime = clock.GetTime().AddDays(1);
    }
    
    private DateTime SelectedDateTime => SelectedDate.Add(SelectedTime!.Value.ToTimeSpan());

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Appointment", out object? value))
        {
            _appointment = (Appointment)value;
            _master = _appointment.Master;
            SelectedDate = _appointment.DateTime.Date;
        }
        else
        {
            _master = (Master)query["Master"];
            await UpdateMasterFreeTimeAsync().ConfigureAwait(false);
        }
        
        PropertyChanged += OnPropertyChangedEventHandler;
        await UpdateMasterFreeTimeAsync().ConfigureAwait(false);
        await RefreshAsync().ConfigureAwait(false);
        
        if (_appointment is null) return;
        foreach (SelectableService service in Services)
        {
            if (_appointment.Services.TrueForAll(s => s.Id != service.Value.Id)) continue;
            service.IsSelected = true;
        }

        SelectedTime = TimeOnly.FromTimeSpan(_appointment.DateTime.TimeOfDay);
    }

    private void OnPropertyChangedEventHandler(object? s, PropertyChangedEventArgs args)
    {
        if (args.PropertyName is not nameof(SelectedDate)) return;
        SelectedTime = null;
        UpdateMasterFreeTimeAsync().SafeFireAndForget();
    }

    [RelayCommand]
    private async Task RefreshAsync(CancellationToken cancellationToken = default)
    {
        if (IsRefreshing) return;
        IsRefreshing = true;
        Guard.IsNotNull(_master, nameof(_master));
        UnsubscribeOnServices();
        try
        {
            List<Service> response = await _mediator
                .Send(new GetServicesByMasterIdQuery(_master.Id), cancellationToken)
                .ConfigureAwait(false);
            Services = [.. response.ConvertAll(service => new SelectableService(service))];
        }
        finally
        {
            IsRefreshing = false;
        }
        SubscribeOnServices();
    }

    [RelayCommand]
    private Task GoToConfirmationPageAsync(CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(_master, nameof(_master));
        Guard.IsNotNull(_globalContext.Customer, nameof(_globalContext.Customer));

        List<Service> selectedServices = Services
            .Where(service => service.IsSelected = true)
            .Select(service => service.Value)
            .ToList();

        return Shell.Current.GoToAsync(nameof(ConfirmAppointmentViewModel), new Dictionary<string, object>
        {
            { "Appointment", new Appointment(SelectedDateTime, _master, _globalContext.Customer.Id, selectedServices) }
        }).WaitAsync(cancellationToken);
    }
    
    private Task UpdateMasterFreeTimeAsync(CancellationToken cancellationToken = default)
    {
        if (!_updateMasterFreeTimeTask.IsCompleted) return _updateMasterFreeTimeTask;
        
        Guard.IsNotNull(_master, nameof(_master));
        Guid appointmentId = _appointment?.Id ?? Guid.Empty;
        
        _updateMasterFreeTimeTask = Task.Run(async () =>
            MasterFreeTime = await _masterScheduleService
                .GetMasterFreeTimeByDateAsync(_master.Id, SelectedDate, appointmentId, cancellationToken)
                .ConfigureAwait(false), cancellationToken);
        
        return _updateMasterFreeTimeTask;
    }
    
    private void SubscribeOnServices()
    {
        foreach (SelectableService service in Services.AsSpan())
        {
            service.PropertyChanged += OnServiceOnPropertyChanged;
        }
    }

    private void UnsubscribeOnServices()
    {
        foreach (SelectableService service in Services.AsSpan())
        {
            service.PropertyChanged -= OnServiceOnPropertyChanged;
        }
    }
    
    private void OnServiceOnPropertyChanged(object? s, PropertyChangedEventArgs args)
    {
        SelectedTime = null;
        UpdateMasterFreeTimeAsync().SafeFireAndForget();
    }
}