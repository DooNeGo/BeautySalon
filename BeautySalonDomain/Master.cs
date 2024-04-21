namespace BeautySalon.Domain;

public sealed record Master
{
    private Master() { }

    public Master(string lastName, string firstName, string? middleName, Position position, string phone)
    {
        Id = Guid.Empty;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Position = position;
        Phone = phone;
    }

    public Guid Id { get; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public Position Position { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public Salon Salon { get; set; } = null!;
}