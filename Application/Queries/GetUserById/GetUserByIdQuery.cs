using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IQuery<User?>;