using System.Collections.ObjectModel;

namespace BeautySalon.Domain;

public sealed record Salon
{
    private Salon() { }

    public Salon(string address, TimeOnly startTime, TimeOnly endTime)
    {
        Id = Guid.NewGuid();
        Address = address;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Guid Id { get; }

    public string Address { get; set; } = string.Empty;
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }

    public List<Master> Masters { get; set; } = [];

    public List<Position> Positions { get; set; } = [];

    public List<Service> Services { get; set; } = [];
}