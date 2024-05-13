using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetMastersQuery(Guid SalonId) : IQuery<List<Master>>;

public sealed class GetMastersQueryHandler(IApplicationContext context) : IQueryHandler<GetMastersQuery, List<Master>>
{
    public ValueTask<List<Master>> Handle(GetMastersQuery query, CancellationToken cancellationToken) =>
        new(context.Masters
            .AsNoTracking()
            .Where(m => m.SalonId == query.SalonId)
            .Include(m => m.Position)
            .ToListAsync(cancellationToken));
}