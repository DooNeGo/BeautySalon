using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.UpdateUser;

public readonly record struct UpdateUserCommand(User UpdatedUser) : ICommand;
