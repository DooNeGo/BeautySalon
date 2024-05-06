using System.Collections.ObjectModel;

namespace BeautySalon.Domain;

public sealed record Salon
{
    private Salon() { }

    public Salon(string address)
    {
        Id = Guid.NewGuid();
        Address = address;
    }

    public Guid Id { get; }

    public string Address { get; set; } = string.Empty;

    public List<Master> Masters { get; set; } = [];

    public List<Position> Positions { get; set; } = [];

    public List<Service> Services { get; set; } = [];
}