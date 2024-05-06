namespace BeautySalon.Domain;

public sealed record Master
{
    private Master() { }

    public Master(string lastName, string firstName, string? middleName, string phone)
    {
        Id = Guid.NewGuid();
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Phone = phone;
    }

    public Guid Id { get; }

    public string LastName { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string? MiddleName { get; set; }

    public Position Position { get; set; } = null!;
    
    public Guid PositionId { get; set; }

    public string Phone { get; set; } = string.Empty;

    public Salon Salon { get; set; } = null!;
    
    public Guid SalonId { get; set; }
    
    public List<Service> Services = [];
}