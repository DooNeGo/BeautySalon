namespace BeautySalon.Domain;

public sealed record City
{
    private City() { }

    public City(string name)
    {
        Name = name;
    }

    public int Id { get; }

    public string Name { get; set; } = null!;

    public List<Salon> Salons { get; } = null!;
}