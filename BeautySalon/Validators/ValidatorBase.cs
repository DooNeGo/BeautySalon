namespace BeautySalon.Validators;

internal abstract class ValidatorBase<T>
{
    public abstract Exception Validate(T value);
}
