namespace BeautySalon.Domain;

public sealed record Position
{
    private Position() { }

    public Position(string title, Salon salon)
    {
        Id = Guid.NewGuid();
        Title = title;
        Salon = salon;
    }

    public Guid Id { get; }

    public string Title { get; set; } = null!;

    public Salon Salon { get; set; } = null!;
}