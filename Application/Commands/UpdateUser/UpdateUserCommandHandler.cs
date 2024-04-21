using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Commands.UpdateUser;

public sealed class UpdateUserCommandHandler(IApplicationContext context) : ICommandHandler<UpdateUserCommand>
{
    public async ValueTask<Unit> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FirstOrDefaultAsync(p => p.Id == command.UpdatedUser.Id, cancellationToken);
        if (user is not null)
        {
            user.Username = command.UpdatedUser.Username;
            user.Password = command.UpdatedUser.Password;
            user.Customer = command.UpdatedUser.Customer;
            user.Email = command.UpdatedUser.Email;

            await context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}
