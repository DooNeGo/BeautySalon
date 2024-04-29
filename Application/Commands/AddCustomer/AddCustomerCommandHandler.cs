using BeautySalon.Application.Interfaces;
using Mediator;

namespace BeautySalon.Application.Commands.AddCustomer;

public sealed class AddCustomerCommandHandler(IApplicationContext context) : ICommandHandler<AddCustomerCommand, Guid>
{
    public ValueTask<Guid> Handle(AddCustomerCommand command, CancellationToken cancellationToken)
    {
        context.Customers.Add(command.Customer);
        throw new NotImplementedException();
    }
}