using BeautySalon.Application.Interfaces;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetServicesCountQuery(Guid SalonId) : IQuery<int>;

public sealed class GetServicesCountQueryHandler(IApplicationContext context)
    : IQueryHandler<GetServicesCountQuery, int>
{
    public ValueTask<int> Handle(GetServicesCountQuery query, CancellationToken cancellationToken) =>
        new(context.Salons
            .Where(s => s.Id == query.SalonId)
            .SelectMany(s => s.Services)
            .CountAsync(cancellationToken));
}