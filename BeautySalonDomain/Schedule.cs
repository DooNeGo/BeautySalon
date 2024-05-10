namespace BeautySalon.Domain;

public sealed record Schedule
{
    public Guid ScheduleId { get; }
    
    public Guid MasterId { get; }
    
    public IReadOnlyList<Appointment> Appointments { get; }

    public void AddAppointment(Appointment appointment)
    {
        
    }
}