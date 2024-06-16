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
using IQueryAttributable = Microsoft.Maui.Controls.IQueryAttributable;

namespace BeautySalon.UI.ViewModel;

public partial class SelectableObjectWrapper<T>(T value) : ObservableObject
{
    [ObservableProperty] private bool _isSelected;

    public T Value => value;
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
        (_mediator, _globalContext, _masterScheduleService) = (mediator, globalContext, masterScheduleService);
        MinimumDateTime = clock.GetTime().AddDays(1);
    }

    public bool IsAnyServiceSelected => Services.Any(service => service.IsSelected);
    
    private DateTime SelectedDateTime => SelectedDate.Add(SelectedTime!.Value.ToTimeSpan());

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("Appointment", out object? value))
        {
            _appointment = (Appointment)value;
            _master = _appointment.Master;
        }
        else
        {
            _master = (Master)query["Master"];
        }
        
        InitializeAsync().SafeFireAndForget();
    }

    private async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await UpdateMasterFreeTimeAsync(cancellationToken).ConfigureAwait(false);
        await UpdatedServicesAsync(cancellationToken).ConfigureAwait(false);
        
        if (_appointment is not null)
        {
            InitializeSelectedServices();
            SelectedDate = _appointment.DateTime.Date;
            SelectedTime = TimeOnly.FromTimeSpan(_appointment.DateTime.TimeOfDay);
        }
        
        PropertyChanged += OnPropertyChangedEventHandler;
    }

    private void InitializeSelectedServices()
    {
        Guard.IsNotNull(_appointment, nameof(_appointment));
        IEnumerable<SelectableService> selectedServices = Services.Where(service => 
            !_appointment.Services.TrueForAll(s => s.Id != service.Value.Id));

        foreach (SelectableService service in selectedServices)
        {
            service.IsSelected = true;
        }
    }

    private void OnPropertyChangedEventHandler(object? s, PropertyChangedEventArgs args)
    {
        if (args.PropertyName is not nameof(SelectedDate)) return;
        SelectedTime = null;
        UpdateMasterFreeTimeAsync().SafeFireAndForget();
    }

    [RelayCommand]
    private async Task UpdatedServicesAsync(CancellationToken cancellationToken = default)
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
            .Where(service => service.IsSelected)
            .Select(service => service.Value)
            .ToList();
        
        Dictionary<string, object?> parameters = new()
        {
            { "Action", _appointment is null ? Action.Add : Action.Update }
        };

        if (_appointment is not null)
        {
            parameters.Add("Appointment", new Appointment(SelectedDateTime,
                _master, _globalContext.Customer.Id, selectedServices) 
                { Id = _appointment.Id });   
        }
        else
        {
            parameters.Add("Appointment", new Appointment(SelectedDateTime,
                _master, _globalContext.Customer.Id, selectedServices));
        }

        return Shell.Current
            .GoToAsync(nameof(ConfirmAppointmentViewModel), parameters)
            .WaitAsync(cancellationToken);
    }
    
    private Task UpdateMasterFreeTimeAsync(CancellationToken cancellationToken = default)
    {
        if (!_updateMasterFreeTimeTask.IsCompleted) return _updateMasterFreeTimeTask;
        
        Guard.IsNotNull(_master, nameof(_master));
        Guid appointmentId = _appointment?.Id ?? Guid.Empty;

        _updateMasterFreeTimeTask = Task.Run(async () => MasterFreeTime = await _masterScheduleService
            .GetMasterFreeTimeByDateAsync(_master.Id, SelectedDate, appointmentId, cancellationToken)
            .ConfigureAwait(false), cancellationToken);
        
        return _updateMasterFreeTimeTask;
    }
    
    private void SubscribeOnServices()
    {
        foreach (SelectableService service in Services.AsSpan())
        {
            service.PropertyChanged += ServiceOnPropertyChanged;
        }
    }
    
    private void UnsubscribeOnServices()
    {
        foreach (SelectableService service in Services.AsSpan())
        {
            service.PropertyChanged -= ServiceOnPropertyChanged;
        }
    }

    private void ServiceOnPropertyChanged(object? sender, PropertyChangedEventArgs e) =>
        OnPropertyChanged(nameof(IsAnyServiceSelected));
}