namespace BeautySalon.Domain;

public sealed record Salon
{
    private Salon() { }

    public Salon(string address)
    {
        Id = Guid.Empty;
        Address = address;
    }

    public Guid Id { get; }

    public string Address { get; set; } = null!;

    public List<Master> Masters { get; set; } = [];

    public List<Position> Positions { get; set; } = [];

    public List<Service> Services { get; set; } = [];
}