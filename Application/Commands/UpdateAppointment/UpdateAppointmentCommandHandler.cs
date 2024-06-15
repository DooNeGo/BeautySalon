using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Commands.UpdateAppointment;

public sealed class UpdateAppointmentCommandHandler(IApplicationContext context)
    : ICommandHandler<UpdateAppointmentCommand>
{
    public async ValueTask<Unit> Handle(UpdateAppointmentCommand command, CancellationToken cancellationToken)
    {
        Appointment appointment = await context.Appointments
            .AsTracking()
            .Where(appointment => appointment.Id == command.Appointment.Id)
            .FirstAsync(cancellationToken);

        Appointment updatedAppointment = command.Appointment;
        appointment.DateTime = updatedAppointment.DateTime;
        appointment.Services = updatedAppointment.Services;
        
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return Unit.Value;
    }
}