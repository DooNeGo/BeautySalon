using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUserCommand;

public sealed class AddUserCommandHandler(IApplicationContext context) : ICommandHandler<AddUserCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        User value = (await context.Users.AddAsync(command.User, cancellationToken)).Entity;
        await context.SaveChangesAsync(cancellationToken);

        return value.Id;
    }
}