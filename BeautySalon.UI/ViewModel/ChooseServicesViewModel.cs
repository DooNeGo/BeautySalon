using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;
using System.Collections.ObjectModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ChooseServicesViewModel : ObservableObject, IQueryAttributable
{
    private const int TimeStepInMinutes = 30;

    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;

    [ObservableProperty]
    private TimeOnly? _selectedTime;

    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private IReadOnlyList<Service> _services = [];
    [ObservableProperty] private ObservableCollection<Service> _selectedServices = [];
    [ObservableProperty] private IReadOnlyList<TimeOnly> _masterFreeTime = [];
    [ObservableProperty] private DateTime _minimumDateTime;
    [ObservableProperty] private bool _isRefreshing;
    [ObservableProperty] private bool _isErrorVisible;

    private Master? _master;

    public ChooseServicesViewModel(IMediator mediator, IClock clock, GlobalContext globalContext)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        MinimumDateTime = clock.GetTime().AddDays(1);

        SelectedServices.CollectionChanged += async (sender, e) =>
        {
            SelectedTime = null;
            MasterFreeTime = await GetMasterFreeTimeAsync().ConfigureAwait(false);
        };

        PropertyChanged += async (_, args) =>
        {
            if (args.PropertyName is nameof(SelectedDate))
            {
                MasterFreeTime = await GetMasterFreeTimeAsync().ConfigureAwait(false);
            }
        };
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _master = (Master)query["Master"];
        await Refresh(CancellationToken.None).ConfigureAwait(false);
    }

    [RelayCommand]
    private async Task Refresh(CancellationToken cancellationToken)
    {
        Services = await _mediator
            .Send(new GetServicesByMasterIdQuery(_master!.Id), cancellationToken)
            .ConfigureAwait(false);
        IsRefreshing = false;
    }

    [RelayCommand]
    private void AddService(Service service)
    {
        if (SelectedServices.Count is 5) IsErrorVisible = true;
        else SelectedServices.Add(service);
    }

    [RelayCommand]
    private void RemoveService(Service service)
    {
        if (IsErrorVisible) IsErrorVisible = false;
        SelectedServices.Remove(service);
    }

    private async Task<IReadOnlyList<TimeOnly>> GetMasterFreeTimeAsync()
    {
        if (_master is null) return [];

        List<TimeOnly> workTime = GetWorkTime();
        List<Appointment> appointments = await _mediator
            .Send(new GetAppointmentsByMasterIdQuery(_master.Id, SelectedDate))
            .ConfigureAwait(false);

        foreach (Appointment appointment in appointments)
        {
            foreach (Service service in appointment.Services)
            {
                TimeOnly start = TimeOnly.FromTimeSpan(appointment.DateTime.TimeOfDay);
                TimeOnly end = start.Add(service.Duration);

                while (start <= end)
                {
                    workTime.Remove(start);
                    start = start.Add(TimeSpan.FromMinutes(TimeStepInMinutes));
                }
            }
        }

        return workTime;
    }

    private List<TimeOnly> GetWorkTime()
    {
        TimeOnly start = _globalContext.Salon.StartTime;
        TimeOnly end = _globalContext.Salon.EndTime;

        List<TimeOnly> workTime = [];

        while (start <= end)
        {
            workTime.Add(start);
            start = start.Add(TimeSpan.FromMinutes(TimeStepInMinutes));
        }

        return workTime;
    }
}