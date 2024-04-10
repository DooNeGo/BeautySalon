using BeautySalon.Application.Commands.AddUserCommand;
using Mediator;

namespace BeautySalon.Application.Validators;

internal sealed class UserValidator : IPipelineBehavior<AddUserCommand, Unit>
{
    public ValueTask<Unit> Handle(AddUserCommand message, CancellationToken cancellationToken, MessageHandlerDelegate<AddUserCommand, Unit> next)
    {
        throw new NotImplementedException();
    }
}
