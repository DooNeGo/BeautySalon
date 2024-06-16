using BeautySalon.Application.Interfaces;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Application.Commands.DeleteAppointment;

public sealed record DeleteAppointmentCommand(Guid AppointmentId) : ICommand<int>;

public sealed class DeleteAppointmentCommandHandler(IApplicationContext context) : ICommandHandler<DeleteAppointmentCommand, int>
{
    public ValueTask<int> Handle(DeleteAppointmentCommand command, CancellationToken cancellationToken) =>
        new(context.Appointments
            .Where(appointment => appointment.Id == command.AppointmentId)
            .ExecuteDeleteAsync(cancellationToken));
}