using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetAppointmentsByMasterIdAndDateQuery(Guid MasterId, DateTime Date) : IQuery<List<Appointment>>;

public sealed class GetAppointmentsByMasterIdAndDateQueryHandler(IApplicationContext context)
    : IQueryHandler<GetAppointmentsByMasterIdAndDateQuery, List<Appointment>>
{
    public ValueTask<List<Appointment>> Handle(GetAppointmentsByMasterIdAndDateQuery query,
        CancellationToken cancellationToken) =>
        new(context.Appointments
            .Where(appointment => appointment.Master.Id == query.MasterId && appointment.DateTime.Date == query.Date.Date)
            .Include(appointment => appointment.Services)
            .ToListAsync(cancellationToken));
}