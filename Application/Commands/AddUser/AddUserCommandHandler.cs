using Mediator;

namespace BeautySalon.Application.Commands.AddUserCommand;

public sealed class AddUserCommandHandler(IApplicationContext context) : ICommandHandler<AddUserCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        Guid id = (await context.Users.AddAsync(command.User, cancellationToken)).Entity.Id;
        await context.SaveChangesAsync(cancellationToken);

        return id;
    }
}