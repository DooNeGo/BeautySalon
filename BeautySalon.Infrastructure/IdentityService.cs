using BeautySalon.Application;
using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure;

public class IdentityService(IApplicationContext context) : IIdentityService
{
    public User? CurrentUser { get; set; }

    public event Action<User>? LoginSuccesful;

    public async Task<bool> AuthorizeAsync(string username, string password)
    {
        CurrentUser = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Username == username && p.Password == password);

        if (CurrentUser is not null)
        {
            LoginSuccesful?.Invoke(CurrentUser);
        }

        return CurrentUser is not null;
    }
}