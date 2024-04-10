using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUserCommand;

public sealed class AddUserCommandHandler(IApplicationContext context) : ICommandHandler<AddUserCommand>
{
    public async ValueTask<Unit> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        await context.Users.AddAsync(command.User, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}