using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetMastersByServiceIdQuery(Guid ServiceId, Guid SalonId) : IQuery<List<Master>>;

public sealed class GetMastersByServiceIdQueryHandler(IApplicationContext context) : IQueryHandler<GetMastersByServiceIdQuery, List<Master>>
{
    public ValueTask<List<Master>> Handle(GetMastersByServiceIdQuery query, CancellationToken cancellationToken) =>
        new(context.Services
            .Where(s => s.Id == query.ServiceId)
            .SelectMany(s => s.Positions)
            .SelectMany(p => p.Masters)
            .Where(m => m.SalonId == query.SalonId)
            .Include(m => m.Position)
            .ToListAsync(cancellationToken));
}