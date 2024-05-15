using BeautySalon.Application.Interfaces;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetMastersCountQuery(Guid SalonId) : IQuery<int>;

public sealed class GetMastersCountQueryHandler(IApplicationContext context) : IQueryHandler<GetMastersCountQuery, int>
{
    public ValueTask<int> Handle(GetMastersCountQuery query, CancellationToken cancellationToken) =>
        new(context.Masters
            .AsNoTracking()
            .Where(master => master.SalonId == query.SalonId)
            .CountAsync(cancellationToken));
}