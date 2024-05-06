using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddCustomer;

public sealed class AddCustomerCommandHandler(IApplicationContext context) : ICommandHandler<AddCustomerCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        Guid id = (await context.Customers.AddAsync(command.Customer, cancellationToken)).Entity.Id;
        await context.SaveChangesAsync(cancellationToken);
        return id;
    }
}