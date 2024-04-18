namespace BeautySalon.Domain;

public sealed record User
{
    private User() { }

    public User(string username, string password, string email, Customer customer)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Customer = customer;
        Email = email;
    }

    public Guid Id { get; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Customer Customer { get; set; } = null!;
}