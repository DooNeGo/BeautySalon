namespace BeautySalon.Domain;

public sealed record User
{
    private User() { }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;
}