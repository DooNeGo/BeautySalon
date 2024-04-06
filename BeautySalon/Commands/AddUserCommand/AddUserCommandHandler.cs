using BeautySalonDomain;
using LanguageExt.Common;
using Mediator;

namespace BeautySalon.Commands.AddUserCommand;

internal class AddUserCommandHandler(BeautySalonDBContext context) : ICommandHandler<AddUserCommand>
{
    public ValueTask<Unit> Handle(AddUserCommand command, CancellationToken cancellationToken)
    {
        context.Users.Add(new User());
        return Unit.ValueTask;
    }
}
