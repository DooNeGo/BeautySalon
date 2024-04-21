using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Queries.GetUser;

public readonly record struct GetUserByIdQuery(Guid Id) : IQuery<User?>;
