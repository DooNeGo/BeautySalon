using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Commands.AddAppointment;

internal sealed class AddAppointmentCommandHandler(IApplicationContext context)
    : ICommandHandler<AddAppointmentCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddAppointmentCommand command, CancellationToken cancellationToken)
    {
        Master master = await context.Masters
            .AsTracking()
            .Where(master => master.Id == command.Appointment.Master.Id)
            .FirstAsync(cancellationToken)
            .ConfigureAwait(false);

        master.Appointments.Add(command.Appointment);
        
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return command.Appointment.Id;
    }
}