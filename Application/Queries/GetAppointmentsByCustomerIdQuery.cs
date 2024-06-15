using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetAppointmentsByCustomerIdQuery(Guid CustomerId) : IQuery<List<Appointment>>;

public sealed class GetAppointmentsByCustomerIdQueryHandler(IApplicationContext context)
    : IQueryHandler<GetAppointmentsByCustomerIdQuery, List<Appointment>>
{
    public ValueTask<List<Appointment>> Handle(GetAppointmentsByCustomerIdQuery query, CancellationToken cancellationToken) =>
        new(context.Appointments
            .Where(appointment => appointment.Customer.Id == query.CustomerId)
            .Include(appointment => appointment.Services)
            .Include(appointment => appointment.Master)
            .ThenInclude(master => master.Position)
            .ToListAsync(cancellationToken: cancellationToken));
}