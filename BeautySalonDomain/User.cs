namespace BeautySalon.Domain;

public sealed record User
{
    private User() { }

    public User(string username, string password, Customer customer)
    {
        Username = username;
        Password = password;
        Customer = customer;
    }

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public Customer Customer { get; set; } = null!;
}