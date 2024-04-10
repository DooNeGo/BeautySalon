namespace BeautySalon.Domain;

public sealed record Customer
{
    private Customer() { }

    public Customer(string surname, string name, string middleName, string contactInfo)
    {
        Surname = surname;
        Name = name;
        MiddleName = middleName;
        ContactInfo = contactInfo;
    }

    public int Id { get; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string ContactInfo { get; set; } = null!;
}