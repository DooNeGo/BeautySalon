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
        context.Appointments.Add(command.Appointment);
        await context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return command.Appointment.Id;
    }
}