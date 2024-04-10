namespace BeautySalon.Domain;

public sealed record Service
{
    private Service() { }

    public Service(string name, decimal price, TimeSpan duration, ServiceType type)
    {
        Name = name;
        Price = price;
        Duration = duration;
        Type = type;
    }

    public int Id { get; }

    public string Name { get; set; } = null!;
    
    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }

    public ServiceType Type { get; set; } = null!;

    public List<Appointment> Appointments { get; set; } = null!;
}
