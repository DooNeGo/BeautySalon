using BeautySalon.Application.Interfaces;

namespace BeautySalon.Infrastructure;

internal sealed class Clock : IClock
{
    public DateTime GetTime() => DateTime.Now;
}