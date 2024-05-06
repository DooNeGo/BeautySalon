namespace BeautySalon.Domain;

public sealed record User
{
    private User() { }

    public User(string username, string password, string email)
    {
        Id = Guid.NewGuid();
        Username = username;
        Password = password;
        Email = email;
    }

    public Guid Id { get; }

    public string Username { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public Customer? Customer { get; set; }
    
    public Guid? CustomerId { get; set; }
}