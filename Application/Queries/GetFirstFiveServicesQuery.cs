using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetFirstFiveServicesQuery(Guid SalonId) : IQuery<List<Service>>;

public sealed class GetFirstFiveServicesQueryHandler(IApplicationContext context) : IQueryHandler<GetFirstFiveServicesQuery, List<Service>>
{
    public ValueTask<List<Service>> Handle(GetFirstFiveServicesQuery query, CancellationToken cancellationToken) =>
        new(context.Services
            .AsNoTracking()
            .Where(s => s.Salons.Any(p => p.Id == query.SalonId))
            .Take(5)
            .ToListAsync(cancellationToken));
}