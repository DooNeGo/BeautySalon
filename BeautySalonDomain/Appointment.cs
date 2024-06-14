using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain;

public sealed record Appointment
{
    private Appointment() { }

    public Appointment(DateTime dateTime, Master master, Guid customerId, List<Service> services)
    {
        Id = Guid.NewGuid();
        DateTime = dateTime;
        Master = master;
        Services = services;
        CustomerId = customerId;
    }

    public Guid Id { get; }

    public DateTime DateTime { get; set; }

    public Master Master { get; set; } = null!;
    
    public Guid MasterId { get; init; }

    public List<Service> Services { get; set; } = [];

    public Customer Customer { get; set; } = null!;
    
    public Guid CustomerId { get; init; }

    [NotMapped]
    public decimal TotalPrice => Services.Sum(service => service.Price);

    [NotMapped]
    public TimeSpan TotalDuration =>
        Services.Aggregate(TimeSpan.Zero, (current, service) => current + service.Duration);

    public bool Equals(Appointment? other) => Id == other?.Id;
}