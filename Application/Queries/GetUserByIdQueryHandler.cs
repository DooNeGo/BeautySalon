using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<User?>;

public sealed class GetUserByIdQueryHandler(IApplicationContext context) : IQueryHandler<GetUserByIdQuery, User?>
{
    public ValueTask<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken) =>
        context.Users.FindAsync([query.Id], cancellationToken);
}