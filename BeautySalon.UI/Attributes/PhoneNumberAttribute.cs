using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BeautySalon.UI.Attributes;

internal sealed partial class PhoneNumberAttribute(string errorMessage) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null || value is not string)
        {
            return null;
        }

        if (GetPhoneRegex().IsMatch((string)value))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult(errorMessage);
        }
    }

    [GeneratedRegex(@"^\+375(25|29|33|44)[0-9]{7}$", RegexOptions.Compiled)]
    private static partial Regex GetPhoneRegex();
}
