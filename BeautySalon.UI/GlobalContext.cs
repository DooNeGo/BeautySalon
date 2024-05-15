using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;

namespace BeautySalon.UI;

public sealed class GlobalContext
{
    public GlobalContext(IIdentityService identityService) =>
        identityService.Authorized += user => Customer = user.Customer!;

    public Salon Salon { get; set; } = null!;

    public Customer Customer { get; private set; } = null!;
}