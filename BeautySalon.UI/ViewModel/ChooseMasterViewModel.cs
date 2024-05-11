using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeautySalon.UI.ViewModel;

public sealed partial class ChooseMasterViewModel : ObservableObject, IQueryAttributable
{
    private const int TimeStepInMinutes = 30;

    private readonly IApplicationContext _context;

    [ObservableProperty] private Service _service = null!;
    [ObservableProperty] private Master[] _masters = [];
    [ObservableProperty] private string[] _masterFreeTime = [];
    [ObservableProperty] private Master? _selectedMaster;
    [ObservableProperty] private DateTime _selectedDate;
    [ObservableProperty] private DateTime _minimumDateTime;

    public ChooseMasterViewModel(IApplicationContext context, IClock clock)
    {
        _context = context;
        MinimumDateTime = clock.GetTime().AddDays(1);

        PropertyChanged += (_, args) =>
        {
            if (args.PropertyName is nameof(SelectedMaster) or nameof(SelectedDate))
            {
                UpdateMasterFreeTime();
            }
        };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Service = (Service)query["Service"];
        Task.Run(() => Masters = [.. _context.Masters.Where(m => m.Position.Services.Contains(Service))]);
    }

    [RelayCommand]
    private void SetMaster(Master master) => SelectedMaster = master;

    private void UpdateMasterFreeTime() =>
        MasterFreeTime = GetMasterFreeTime().Select(p => p.ToString("HH:mm")).ToArray();

    private List<TimeOnly> GetMasterFreeTime()
    {
        if (SelectedMaster is null) return [];

        List<TimeOnly> workTime = GetWorkTime();
        IEnumerable<Appointment> appointments = SelectedMaster.Appointments
            .Where(a => a.DateTime.Date == SelectedDate);

        foreach (Appointment appointment in appointments)
        {
            foreach (Service service in appointment.Services)
            {
                TimeOnly start = TimeOnly.FromTimeSpan(appointment.DateTime.TimeOfDay);
                TimeOnly end = start.Add(service.Duration);
                TimeSpan offset = TimeSpan.Zero;

                while (start.Add(offset) <= end)
                {
                    workTime.Remove(start);
                    offset += TimeSpan.FromMinutes(TimeStepInMinutes);
                }
            }
        }

        return workTime;
    }

    private List<TimeOnly> GetWorkTime()
    {
        TimeOnly start = _context.Salons.First().StartTime;
        TimeOnly end = _context.Salons.First().EndTime;
        TimeSpan offset = TimeSpan.Zero;

        List<TimeOnly> workTime = [];

        while (start.Add(offset) <= end)
        {
            workTime.Add(start);
            offset += TimeSpan.FromMinutes(TimeStepInMinutes);
        }

        return workTime;
    }
}