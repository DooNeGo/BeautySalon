using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetSalonQuery : IQuery<Salon>;

public sealed class GetSalonQueryHandler(IApplicationContext context) : IQueryHandler<GetSalonQuery, Salon>
{
    public ValueTask<Salon> Handle(GetSalonQuery query, CancellationToken cancellationToken) =>
        new(context.Salons.FirstAsync(cancellationToken));
}