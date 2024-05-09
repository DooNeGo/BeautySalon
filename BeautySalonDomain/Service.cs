namespace BeautySalon.Domain;

public sealed record Service
{
    private Service() { }

    public Service(string name, decimal price, TimeSpan duration)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        Duration = duration;
    }

    public Guid Id { get; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }
    
    public List<Position> Positions { get; set; } = [];

    public List<Salon> Salons { get; set; } = [];
}