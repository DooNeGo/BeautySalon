namespace BeautySalon.Domain;

public sealed record Position
{
    private Position() { }

    public Position(string title)
    {
        Id = Guid.NewGuid();
        Title = title;
    }

    public Guid Id { get; }

    public string Title { get; set; } = string.Empty;

    public List<Service> Services { get; set; } = [];

    public List<Master> Masters { get; set; } = [];
}