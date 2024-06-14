using System.Collections.Immutable;
using BeautySalon.Application.Queries;
using BeautySalon.Domain;
using CommunityToolkit.Diagnostics;
using Mediator;

namespace BeautySalon.UI.Services;

public interface IMasterScheduleService
{
    public Task<ImmutableArray<TimeOnly>> GetMasterFreeTimeByDateAsync(Guid masterId, DateTime dateTime,
        CancellationToken cancellationToken = default);

    public Task<ImmutableArray<TimeOnly>> GetMasterFreeTimeByDateAsync(Guid masterId, DateTime dateTime,
        Guid withoutAppointmentId, CancellationToken cancellationToken = default);
}

public sealed class MasterScheduleService : IMasterScheduleService
{
    private const int TimeIntervalInMinutes = 30;
    private static readonly TimeSpan TimeInterval = TimeSpan.FromMinutes(TimeIntervalInMinutes);
    
    private readonly IMediator _mediator;
    private readonly GlobalContext _globalContext;
    private readonly ImmutableArray<TimeOnly> _workIntervals;

    public MasterScheduleService(IMediator mediator, GlobalContext globalContext)
    {
        (_mediator, _globalContext) = (mediator, globalContext);
        _workIntervals = GetWorkIntervals();
    }

    public Task<ImmutableArray<TimeOnly>> GetMasterFreeTimeByDateAsync(Guid masterId, DateTime dateTime,
        CancellationToken cancellationToken = default) =>
        GetMasterFreeTimeByDateAsync(masterId, dateTime, Guid.Empty, cancellationToken);

    public async Task<ImmutableArray<TimeOnly>> GetMasterFreeTimeByDateAsync(Guid masterId,
        DateTime dateTime, Guid withoutAppointmentId, CancellationToken cancellationToken = default)
    {
        List<Appointment> appointments = await _mediator
            .Send(new GetAppointmentsByMasterIdAndDateQuery(masterId, dateTime), cancellationToken)
            .ConfigureAwait(false);

        if (withoutAppointmentId != Guid.Empty)
        {
            int index = appointments.FindIndex(appointment => appointment.Id == withoutAppointmentId);
            appointments.RemoveAt(index);
        }

        return await CalculateMasterFreeTimeAsync(appointments, cancellationToken).ConfigureAwait(false);
    }

    private Task<ImmutableArray<TimeOnly>> CalculateMasterFreeTimeAsync(IReadOnlyList<Appointment> appointments,
        CancellationToken token = default) => Task.Run(() =>
        {
            List<TimeOnly> result = [.. _workIntervals];
            foreach (Appointment appointment in appointments)
            {
                token.ThrowIfCancellationRequested();
                
                TimeOnly start = TimeOnly.FromTimeSpan(appointment.DateTime.TimeOfDay);
                TimeOnly end = start.Add(appointment.TotalDuration);
                
                while (start <= end)
                {
                    result.Remove(start);
                    start = start.Add(TimeInterval);
                }
            }
            return result.ToImmutableArray();
        }, token);

    private ImmutableArray<TimeOnly> GetWorkIntervals()
    {
        Guard.IsNotNull(_globalContext.Salon, nameof(_globalContext.Salon));
        
        TimeOnly start = _globalContext.Salon.StartTime;
        TimeOnly end = _globalContext.Salon.EndTime;
        
        List<TimeOnly> workTime = [];

        while (start <= end)
        {
            workTime.Add(start);
            start = start.Add(TimeInterval);
        }

        return [.. workTime];
    }
}