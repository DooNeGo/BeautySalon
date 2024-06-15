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
        new(context.Salons
            .Where(salon => salon.Id == query.SalonId)
            .SelectMany(salon => salon.Services)
            .ToListAsync(cancellationToken));
}