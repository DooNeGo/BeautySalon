using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetServicesByMasterIdQuery(Guid MasterId) : IQuery<List<Service>>;

public sealed class GetServicesByMasterIdQueryHandler(IApplicationContext context)
    : IQueryHandler<GetServicesByMasterIdQuery, List<Service>>
{
    public ValueTask<List<Service>> Handle(GetServicesByMasterIdQuery query, CancellationToken cancellationToken) =>
        new(context.Masters
            .Where(m => m.Id == query.MasterId)
            .Select(m => m.Position)
            .SelectMany(p => p.Services)
            .ToListAsync(cancellationToken));
}