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
}