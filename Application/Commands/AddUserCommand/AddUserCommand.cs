using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUserCommand;

public readonly struct AddUserCommand(User user) : ICommand<Guid>
{
    public User User => user;
}