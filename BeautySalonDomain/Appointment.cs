namespace BeautySalon.Domain;

public sealed record Appointment
{
    private Appointment() { }

    public Appointment(DateTime dateTime, Master master, Customer customer, List<Service> services)
    {
        Id = Guid.NewGuid();
        DateTime = dateTime;
        Master = master;
        Services = services;
        Customer = customer;
    }

    public Guid Id { get; }

    public DateTime DateTime { get; set; }

    public Master Master { get; set; } = null!;

    public List<Service> Services { get; set; } = [];

    public Customer Customer { get; set; } = null!;
}