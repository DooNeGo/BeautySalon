using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetAppointmentsByMasterIdQuery(Guid MasterId, DateTime Date) : IQuery<List<Appointment>>;

public sealed class GetAppointmentsByMasterIdQueryHandler(IApplicationContext context)
    : IQueryHandler<GetAppointmentsByMasterIdQuery, List<Appointment>>
{
    public ValueTask<List<Appointment>> Handle(GetAppointmentsByMasterIdQuery query,
        CancellationToken cancellationToken) =>
        new(context.Appointments
            .AsNoTracking()
            .Where(appointment =>
                appointment.Master.Id == query.MasterId && appointment.DateTime.Date == query.Date.Date)
            .Include(appointment => appointment.Customer)
            .Include(appointment => appointment.Services)
            .ToListAsync(cancellationToken));
}