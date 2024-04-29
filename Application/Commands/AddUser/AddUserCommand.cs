using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUser;

public record AddUserCommand(User User) : ICommand<Guid>;