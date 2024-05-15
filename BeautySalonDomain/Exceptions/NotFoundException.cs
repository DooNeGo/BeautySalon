namespace BeautySalon.Domain.Exceptions;

public sealed class NotFoundException(string objectName) : Exception
{
    public override string Message { get; } = $"{objectName} not found";
}