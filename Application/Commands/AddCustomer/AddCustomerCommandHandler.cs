using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddCustomer;

public sealed class AddCustomerCommandHandler(IApplicationContext context) : ICommandHandler<AddCustomerCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users.FindAsync([command.UserId], cancellationToken);
        if (user is not null)
        {
            user.Customer = command.Customer;
        }

        await context.SaveChangesAsync(cancellationToken);
        return Guid.Empty;
    }
}