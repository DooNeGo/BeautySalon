using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ChooseMasterViewModel : ObservableObject, IQueryAttributable
{
    private readonly IApplicationContext _context;
    
    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private Master[] _masters = [];
    [ObservableProperty] private string[] _masterFreeTimeStrings = [];
    [ObservableProperty] private Master? _selectedMaster;
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private DateTime _minimumDateTime;
    
    public ChooseMasterViewModel(IApplicationContext context, IClock clock)
    {
        _context = context;
        MinimumDateTime = clock.GetTime().AddDays(1);
        
        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is not nameof(SelectedMaster)) return;
            
            MasterFreeTimeStrings = GetMasterFreeTime()
                .Select(p => p.ToString("HH:mm"))
                .ToArray();
        };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        Task.Run(() =>
        {
            Masters = _context.Masters
                .Where(m => m.Position.Services.Contains(Service))
                .ToArray();
            
            SelectedMaster = Masters.FirstOrDefault();
        });
    }

    private IEnumerable<TimeOnly> GetMasterFreeTime()
    {
        if (SelectedMaster is null) return Enumerable.Empty<TimeOnly>();

        List<TimeOnly> timeIntervals = GetTimeIntervals();
        IEnumerable<Appointment> appointments = SelectedMaster.Appointments
            .Where(a => a.DateTime.Date == SelectedDate);

        foreach (Appointment appointment in appointments)
        {
            foreach (Service service in appointment.Services)
            {
                TimeOnly startTime = TimeOnly.FromTimeSpan(appointment.DateTime.TimeOfDay);
                TimeOnly endTime = startTime.Add(service.Duration);

                while (startTime <= endTime)
                {
                    timeIntervals.Remove(startTime);
                    startTime = startTime.AddMinutes(30);
                }
            }
        }

        return timeIntervals;
    }

    private List<TimeOnly> GetTimeIntervals()
    {
        TimeOnly startTime = _context.Salons.First().StartTime;
        TimeOnly endTime = _context.Salons.First().EndTime;
        
        List<TimeOnly> timeIntervals = [];

        while(startTime <= endTime)
        {
            timeIntervals.Add(startTime);
            startTime = startTime.AddMinutes(30);
        }

        return timeIntervals;
    }
}