namespace BeautySalon.Domain;

public sealed record City
{
    private City() { }

    public City(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; }

    public string Name { get; set; } = null!;

    public List<Salon> Salons { get; set; } = null!;
}