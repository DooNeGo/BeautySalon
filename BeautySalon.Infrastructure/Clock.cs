using BeautySalon.Application.Interfaces;

namespace BeautySalon.Infrastructure;

public sealed class Clock : IClock
{
    public DateTime GetTime() => DateTime.Now;
}