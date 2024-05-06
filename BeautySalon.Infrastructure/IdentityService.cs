using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure;

public sealed class IdentityService(IApplicationContext context) : IIdentityService
{
    public User? CurrentUser { get; private set; }

    public event Action<User>? LoginSuccessful;

    public async Task<bool> AuthorizeAsync(string username, string password)
    {
        CurrentUser = await context.Users.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Username == username && p.Password == password);

        if (CurrentUser is not null)
        {
            LoginSuccessful?.Invoke(CurrentUser);
        }

        return CurrentUser is not null;
    }
}