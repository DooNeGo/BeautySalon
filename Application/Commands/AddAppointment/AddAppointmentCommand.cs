using BeautySalon.Application.Interfaces;
using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddAppointment;

public sealed record AddAppointmentCommand(Appointment Appointment) : ICommand<Guid>;

internal sealed class AddAppointmentCommandHandler(IApplicationContext context)
    : ICommandHandler<AddAppointmentCommand, Guid>
{
    public async ValueTask<Guid> Handle(AddAppointmentCommand command, CancellationToken cancellationToken)
    {
        await context.Appointments.AddAsync(command.Appointment, cancellationToken).ConfigureAwait(false);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return command.Appointment.Id;
    }
}