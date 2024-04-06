namespace BeautySalon.Validators;

internal interface IValidator<in T>
{
    public Exception Validate(T value);
}
