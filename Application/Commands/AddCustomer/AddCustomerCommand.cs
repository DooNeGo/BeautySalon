using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddCustomer;

public record AddCustomerCommand(Customer Customer, Guid UserId) : ICommand<Guid>;