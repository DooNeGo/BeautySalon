using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddCustomerCommand;

public readonly struct AddCustomerCommand(Customer customer) : ICommand<Guid>
{
    public Customer Customer => customer;
}
