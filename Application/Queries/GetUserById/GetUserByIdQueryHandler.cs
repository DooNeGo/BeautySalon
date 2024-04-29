using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries.GetUserById;

public sealed class GetUserByIdQueryHandler(IApplicationContext context) : IQueryHandler<GetUserByIdQuery, User?>
{
    public ValueTask<User?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        return new ValueTask<User?>(context.Users.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken));
    }
}