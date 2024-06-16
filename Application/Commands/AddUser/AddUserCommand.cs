using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddUser;

public sealed record AddUserCommand(User User) : ICommand<Guid>;