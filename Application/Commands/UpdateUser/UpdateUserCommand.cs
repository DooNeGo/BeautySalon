using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.UpdateUser;

public record UpdateUserCommand(User UpdatedUser) : ICommand;