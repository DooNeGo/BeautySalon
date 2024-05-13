using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetServicesQuery(Guid SalonId) : IQuery<List<Service>>;

public sealed class GetServicesQueryHandler(IApplicationContext context)
    : IQueryHandler<GetServicesQuery, List<Service>>
{
    public ValueTask<List<Service>> Handle(GetServicesQuery query, CancellationToken cancellationToken) =>
        new(context.Services
            .AsNoTracking()
            .Where(s => s.Salons.Any(p => p.Id == query.SalonId))
            .ToListAsync(cancellationToken));
}