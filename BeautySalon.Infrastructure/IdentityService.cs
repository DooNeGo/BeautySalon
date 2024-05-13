using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure;

internal sealed class IdentityService(IApplicationContext context) : IIdentityService
{
    public User? CurrentUser { get; private set; }

    public event Action<User>? Authorized; 

    public async Task AuthorizeAsync(string username, string password, CancellationToken cancellationToken)
    {
        CurrentUser = await context.Users
            .AsNoTracking()
            .Where(u => u.Username == username && u.Password == password)
            .Include(u => u.Customer)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (CurrentUser is not null) Authorized?.Invoke(CurrentUser);
    }
}