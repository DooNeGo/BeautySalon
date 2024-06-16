using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BeautySalon.Application.Commands.AddUser;

public sealed class AddUserCommandHandler(IApplicationContext context) : ICommandHandler<AddUserCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        EntityEntry<User> entity = await context.Users.AddAsync(command.User, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return entity.Entity.Id;
    }
}