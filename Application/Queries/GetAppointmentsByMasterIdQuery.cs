using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Queries;

public sealed record GetAppointmentsByMasterIdQuery(Guid MasterId, DateTime Date) : IQuery<List<Appointment>>;

public sealed class GetAppointmentsByMasterIdQueryHandler(IApplicationContext context) : IQueryHandler<GetAppointmentsByMasterIdQuery, List<Appointment>>
{
    public ValueTask<List<Appointment>> Handle(GetAppointmentsByMasterIdQuery query,
        CancellationToken cancellationToken) => 
        new(context.Masters
            .AsNoTracking()
            .Where(m => m.Id == query.MasterId)
            .SelectMany(m => m.Appointments)
            .Where(a => a.DateTime.Date == query.Date.Date)
            .Include(a => a.Customer)
            .Include(a => a.Services)
            .ToListAsync(cancellationToken));
}