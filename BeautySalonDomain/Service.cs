namespace BeautySalon.Domain;

public sealed record Service
{
    private Service() { }

    public Service(string name, decimal price, TimeSpan duration)
    {
        Id = Guid.Empty;
        Name = name;
        Price = price;
        Duration = duration;
    }

    public Guid Id { get; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public TimeSpan Duration { get; set; }

    public List<Master> Masters { get; set; } = [];
}