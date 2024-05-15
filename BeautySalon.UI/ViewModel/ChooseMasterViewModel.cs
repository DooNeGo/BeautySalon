using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ChooseMasterViewModel : ObservableObject, IQueryAttributable
{
    private const int TimeStepInMinutes = 30;

    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private Master? _selectedMaster;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private TimeOnly? _selectedTime;
    
    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private IReadOnlyList<Master> _masters = [];
    [ObservableProperty] private IReadOnlyList<TimeOnly> _masterFreeTime = [];
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private DateTime _minimumDateTime;

    public ChooseMasterViewModel(IMediator mediator, IClock clock, GlobalContext globalContext)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        MinimumDateTime = clock.GetTime().AddDays(1);
        
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is nameof(SelectedMaster) or nameof(SelectedDate))
            {
                 Task.Run(UpdateMasterFreeTimeAsync);
            }
        };
    }

    private DateTime SelectedDateTime => SelectedDate.Add(SelectedTime!.Value.ToTimeSpan());

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        Task.Run(async () =>
        {
            Masters = await _mediator.Send(new GetMastersByServiceIdQuery(Service.Id, _globalContext.Salon.Id));
            //SelectedMaster = Masters[0];
        });
    }

    [RelayCommand]
    private void SetMaster(Master master) => SelectedMaster = master;

    [RelayCommand(CanExecute = nameof(CanGoNext))]
    private async Task GoNext() =>
        await Shell.Current.GoToAsync(nameof(ConfirmAppointmentViewModel),
            new Dictionary<string, object>
            {
                {
                    "Appointment",
                    new Appointment(SelectedDateTime, SelectedMaster!, _globalContext.Customer, [Service])
                }
            });

    private bool CanGoNext() => SelectedMaster is not null && SelectedTime is not null;

    private async Task UpdateMasterFreeTimeAsync() => MasterFreeTime = await GetMasterFreeTimeAsync();

    private async Task<IReadOnlyList<TimeOnly>> GetMasterFreeTimeAsync()
    {
        if (SelectedMaster is null) return [];

        List<TimeOnly> workTime = GetWorkTime();
        List<Appointment> appointments =
            await _mediator.Send(new GetAppointmentsByMasterIdQuery(SelectedMaster.Id, SelectedDate));

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