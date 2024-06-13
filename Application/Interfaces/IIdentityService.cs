using BeautySalon.Domain;

namespace BeautySalon.Application.Interfaces;

public interface IIdentityService
{
    public User? CurrentUser { get; }
    
    public event Action<User>? Authorized;
    public event Action? Unauthorized;

    public Task AuthorizeAsync(string username, string password, CancellationToken cancellationToken);

    public void Unauthorize();
}