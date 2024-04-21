namespace BeautySalon.Domain;

public sealed record User
{
    private User() { }

    public User(string username, string password, string email)
    {
        Id = Guid.Empty;
        Username = username;
        Password = password;
        Email = email;
    }

    public Guid Id { get; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Customer? Customer { get; set; }
}