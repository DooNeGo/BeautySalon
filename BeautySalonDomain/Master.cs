namespace BeautySalon.Domain;

public sealed record Master
{
    private Master() { }

    public Master(string surname, string name, string? middleName, Salon salon, Position position, string contactInfo, byte[] photo)
    {
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        Salon = salon;
        Position = position;
        ContactInfo = contactInfo;
        Photo = photo;
    }

    public int Id { get; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? MiddleName { get; set; }

    public Salon Salon { get; set; } = null!;

    public Position Position { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;

    public byte[]? Photo { get; set; }
}