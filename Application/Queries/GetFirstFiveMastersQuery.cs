using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetFirstFiveMastersQuery(Guid SalonId) : IQuery<List<Master>>;

public sealed class GetFirstFiveMastersQueryHandler(IApplicationContext context) : IQueryHandler<GetFirstFiveMastersQuery, List<Master>>
{
    public ValueTask<List<Master>> Handle(GetFirstFiveMastersQuery query, CancellationToken cancellationToken) =>
        new (context.Masters
            .AsNoTracking()
            .Where(m => m.SalonId == query.SalonId)
            .Take(5)
            .Include(m => m.Position)
            .ToListAsync(cancellationToken));
}