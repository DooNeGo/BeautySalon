namespace BeautySalon.Domain;

public sealed record Position
{
    private Position() { }

    public Position(string title)
    {
        Id = Guid.Empty;
        Title = title;
    }

    public Guid Id { get; }

    public string Title { get; set; } = null!;

    public Salon Salon { get; set; } = null!;
}