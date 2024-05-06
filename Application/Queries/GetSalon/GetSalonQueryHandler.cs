using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries.GetSalon;

public sealed class GetSalonQueryHandler(IApplicationContext context) : IQueryHandler<GetSalonQuery, Salon>
{
    public async ValueTask<Salon> Handle(GetSalonQuery query, CancellationToken cancellationToken)
    {
        return await context.Salons.FirstAsync(cancellationToken: cancellationToken);
    }
}