namespace BeautySalon.Domain;

public sealed record Salon
{
    private Salon() { }

    public Salon(string name, string address)
    {
        Id = Guid.Empty;
        Name = name;
        Address = address;
    }

    public Guid Id { get; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public City City { get; set; } = null!;

    public List<Master> Masters { get; set; } = [];

    public List<Position> Positions { get; set; } = [];
}
