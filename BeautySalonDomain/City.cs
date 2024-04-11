namespace BeautySalon.Domain;

public sealed record City
{
    private City() { }

    public City(string name)
    {
        Name = name;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<Salon> Salons { get; set; } = null!;
}