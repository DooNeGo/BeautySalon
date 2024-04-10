namespace BeautySalon.Domain;

public sealed record Position
{
    private Position() { }

    public Position(string title, Salon salon)
    {
        Title = title;
        Salon = salon;
    }

    public int Id { get; }

    public string Title { get; set; } = null!;

    public Salon Salon { get; set; } = null!;
}