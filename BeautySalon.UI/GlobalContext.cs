using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;

namespace BeautySalon.UI;

public sealed class GlobalContext
{
    public GlobalContext(IIdentityService identityService) =>
        identityService.Authorized += user => User = user;

    public Salon Salon { get; set; } = null!;

    public User User { get; set; } = null!;
}