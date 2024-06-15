using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetAppointmentByIdQuery(Guid Id) : IQuery<Appointment>;

public sealed class GetAppointmentByIdQueryHandler(IApplicationContext context)
    : IQueryHandler<GetAppointmentByIdQuery, Appointment>
{
    public ValueTask<Appointment> Handle(GetAppointmentByIdQuery query, CancellationToken cancellationToken) =>
        new(context.Appointments
            .Where(appointment => appointment.Id == query.Id)
            .Include(appointment => appointment.Services)
            .FirstAsync(cancellationToken));
}