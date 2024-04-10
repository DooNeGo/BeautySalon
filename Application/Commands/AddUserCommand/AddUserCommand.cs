using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUserCommand;

public readonly struct AddUserCommand(User user) : ICommand
{
    public User User { get; } = user;
}