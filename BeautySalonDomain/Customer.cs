namespace BeautySalon.Domain;

public sealed record Customer
{
    private Customer() { }

    public Customer(string lastName, string firstName, string middleName, string phone)
    {
        Id = Guid.Empty;
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        Phone = phone;
    }

    public Guid Id { get; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Phone { get; set; } = null!;
}