namespace BeautySalon.Domain;

public sealed record Salon
{
    private Salon() { }

    public Salon(string name, string address, City city)
    {
        Name = name;
        Address = address;
        City = city;
    }

    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public City City { get; set; } = null!;

    public List<Master> Masters { get; set; } = null!;
}
