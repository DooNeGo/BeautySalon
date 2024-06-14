using System.Collections.Immutable;
using System.ComponentModel;
using BeautySalon.Application.Interfaces;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using BeautySalon.UI.Services;
using CommunityToolkit.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mediator;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ChooseMasterViewModel : ObservableObject, IQueryAttributable
{
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    private readonly IMasterScheduleService _masterScheduleService;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    [NotifyPropertyChangedFor(nameof(IsTimePickerEnable))]
    private Master? _selectedMaster;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(GoNextCommand))]
    private TimeOnly? _selectedTime;
    
    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private IReadOnlyList<Master> _masters = [];
    [ObservableProperty] private ImmutableArray<TimeOnly> _masterFreeTime = [];
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private DateTime _minimumDateTime;

    public ChooseMasterViewModel(IMediator mediator, IClock clock, GlobalContext globalContext,
        IMasterScheduleService masterScheduleService)
    {
        _mediator = mediator;
        _globalContext = globalContext;
        _masterScheduleService = masterScheduleService;
        MinimumDateTime = clock.GetTime().AddDays(1);

        PropertyChanged += OnPropertyChangedEventHandler;
    }

    private async void OnPropertyChangedEventHandler(object? _, PropertyChangedEventArgs args)
    {
        if (args.PropertyName is not (nameof(SelectedMaster) or nameof(SelectedDate))) return;
        if (SelectedMaster is null) return;

        SelectedTime = null;
        MasterFreeTime = await _masterScheduleService
            .GetMasterFreeTimeByDateAsync(SelectedMaster.Id, SelectedDate)
            .ConfigureAwait(false);
    }

    public bool IsTimePickerEnable => SelectedMaster is not null;
    
    private DateTime SelectedDateTime => SelectedDate.Add(SelectedTime!.Value.ToTimeSpan());

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        
        Guard.IsNotNull(_globalContext.Salon, nameof(_globalContext.Salon));
        Masters = await _mediator
            .Send(new GetMastersByServiceIdQuery(Service.Id, _globalContext.Salon.Id))
            .ConfigureAwait(false);
    }

    [RelayCommand]
    private void SetMaster(Master master) => SelectedMaster = master;

    [RelayCommand(CanExecute = nameof(CanGoNext))]
    private Task GoNext()
    {
        Guard.IsNotNull(SelectedMaster, nameof(SelectedMaster));
        Guard.IsNotNull(_globalContext.Customer, nameof(_globalContext.Customer));
        return Shell.Current.GoToAsync(nameof(ConfirmAppointmentViewModel),
            new Dictionary<string, object>
            {
                {
                    "Appointment",
                    new Appointment(SelectedDateTime, SelectedMaster, _globalContext.Customer.Id, [Service])
                }
            });
    }

    private bool CanGoNext() => SelectedMaster is not null && SelectedTime is not null;
}