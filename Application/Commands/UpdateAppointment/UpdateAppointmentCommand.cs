using BeautySalon.Domain;
using Mediator;

namespace BeautySalon.Application.Commands.UpdateAppointment;

public sealed record UpdateAppointmentCommand(Appointment Appointment) : ICommand;