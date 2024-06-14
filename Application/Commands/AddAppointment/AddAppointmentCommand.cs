using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.AddAppointment;

public sealed record AddAppointmentCommand(Appointment Appointment) : ICommand<Guid>;